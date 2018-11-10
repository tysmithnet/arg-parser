using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class ParseStrategy : IParseStrategy
    {
        public ParseStrategy(string rootParserId)
        {
            RootParserId = rootParserId.ThrowIfArgumentNull(nameof(rootParserId));
        }

        public virtual List<Parser> GetParserFamily(IContext context, Parser parser)
        {
            var ancestors = context.HierarchyRepository.GetAncestors(parser.Id)
                .Select(x => context.ParserRepository.Get(x));
            var chain = parser.ToEnumerableOfOne().Concat(ancestors).ToList();
            return chain;
        }

        public virtual Parser IdentifyRelevantParser(string[] args, IContext context)
        {
            var ids = GetCommandIdentifyingSubsequence(args, context);
            if (!ids.Any())
            {
                var root = context.HierarchyRepository.GetRoot();
                return context.ParserRepository.Get(root);
            }

            return context.ParserRepository.Get(ids.Last());
        }

        public virtual IParseResult Parse(string[] args, IContext context)
        {
            var parser = IdentifyRelevantParser(args, context);
            var chain = GetParserFamily(context, parser);
            var subcommandSequence = GetCommandIdentifyingSubsequence(args, context);
            if (parser.FactoryFunction == null)
                throw new NoFactoryFunctionException($"No factory function on parser={parser.Id}");
            var instance = parser.FactoryFunction();
            var info = new IterationInfo(args, subcommandSequence.Count);
            try
            {
                while (!info.IsComplete())
                {
                    var allParsersWhoCanDoSomething =
                        chain.Where(c => c.CanConsume(instance, info).NumConsumed > 0).ToList();
                    if (!allParsersWhoCanDoSomething.Any())
                        throw new UnexpectedArgException(
                            $"Encountered an argument that could not be parsed. Argument={info.Current}, Parsers={chain.Select(p => p.Id).Join(", ")}");

                    var consumptionResultsForTheParsersWhoCanConsume = allParsersWhoCanDoSomething
                        .Select(x => x.CanConsume(instance, info)).ToList();
                    var requestThatLimitsConsumption = CreateCanConsumeRequest(instance, chain, info,
                        consumptionResultsForTheParsersWhoCanConsume.ToList());
                    var whoWillConsume = IdentifyParserToConsume(chain, consumptionResultsForTheParsersWhoCanConsume);
                    var consumptionResult = whoWillConsume.Consume(instance, requestThatLimitsConsumption);
                    info = consumptionResult.Info;
                }

                return new ParseResult(instance.ToEnumerableOfOne(), null);
            }
            catch (ParseException e)
            {
                return new ParseResult(null, e.ToEnumerableOfOne());
            }
            finally
            {
                chain.ForEach(c => c.Reset());
            }
        }

        protected internal ConsumptionRequest CreateCanConsumeRequest(object instance, List<Parser> chain,
            IterationInfo currentInfo, IList<ConsumptionResult> canConsumeResults)
        {
            // first try to find a switch because we prioritize them
            var foundParser = IdentifyParserToConsume(chain, canConsumeResults);

            var foundParameter = canConsumeResults.First(r => r.ConsumingParameter.Parent == foundParser);

            var toBeConsumed = currentInfo.FromNowOn().Take(foundParameter.NumConsumed).ToList();
            for (var i = 1;
                i < toBeConsumed.Count;
                i++)
                foreach (var parser in chain)
                {
                    var info = currentInfo.Consume(i);
                    var res = parser.CanConsume(instance, info);
                    if (res.Info > info && res.ConsumingParameter != null && res.ConsumingParameter is Switch)
                        return new ConsumptionRequest(currentInfo, i);
                }

            return new ConsumptionRequest(currentInfo, foundParameter.NumConsumed);
        }

        protected internal IList<string> GetCommandIdentifyingSubsequence(string[] args, IContext context)
        {
            var ids = new List<string>();
            for (var i = 0; i < args.Length; i++)
            {
                var left = i == 0 ? RootParserId : args[i - 1];
                var right = args[i];
                if (context.HierarchyRepository.IsParent(left, right)) ids.Add(right);
                else break;
            }

            return ids;
        }

        private Parser IdentifyParserToConsume(List<Parser> chain, IList<ConsumptionResult> canConsumeResults)
        {
            Parser foundParser = null;
            var switchResults = canConsumeResults.Where(r => r.ConsumingParameter is Switch).ToList();
            if (switchResults.Any())
                foundParser = switchResults.FirstOrDefault(r => chain.Contains(r.ConsumingParameter.Parent))
                    ?.ConsumingParameter.Parent;

            if (foundParser == null)
                foundParser = canConsumeResults.FirstOrDefault(r => chain.Contains(r.ConsumingParameter.Parent))
                    ?.ConsumingParameter
                    .Parent;

            if (foundParser == null)
                throw new ForwardProgressException($"Expected to find a parser to consume, but found none");

            return foundParser;
        }

        public string RootParserId { get; protected internal set; }
    }
}