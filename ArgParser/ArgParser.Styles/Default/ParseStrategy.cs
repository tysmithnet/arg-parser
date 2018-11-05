using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class ParseStrategy : IParseStrategy
    {
        public string RootParserId { get; protected internal set; }

        public ParseStrategy(string rootParserId)
        {
            RootParserId = rootParserId.ThrowIfArgumentNull(nameof(rootParserId));
        }

        public virtual IParseResult Parse(string[] args, IContext context)
        {
            var parser = IdentifyRelevantParser(args, context);
            var subcommandSequence = GetCommandIdentifyingSubsequence(args, context);
            var instance = parser.FactoryFunction();
            var info = new IterationInfo(args, subcommandSequence.Count);
            try
            {
                while (!info.IsComplete())
                {
                    var chain = GetParserFamily(context, parser);
                    var canHandleMap = chain.ToDictionary(c => c, c => c.CanConsume(instance, info));
                    var firstWhoCanHandle = chain.FirstOrDefault(c => c.CanConsume(instance, info).NumConsumed > 0);
                    if (firstWhoCanHandle == null)
                    {
                        throw new UnexpectedArgException($"Encountered an argument that could not be parsed. Argument={info.Current}, Parsers={chain.Select(p => p.Id).Join(", ")}");
                    }

                    var canConsumeResult = firstWhoCanHandle.CanConsume(instance, info);
                    var request = CreateCanConsumeRequest(instance, chain, info, canConsumeResult);
                    var consumptionResult = firstWhoCanHandle.Consume(instance, request);
                    if (consumptionResult.Info <= info)
                        throw new ForwardProgressException($"Consumption resuled in new index={consumptionResult.Info.Index} and provided index={info.Index}");
                    info = consumptionResult.Info;
                }
                return new ParseResult(instance.ToEnumerableOfOne(), null);
            }
            catch (ParseException e)
            {
                return new ParseResult(null, e.ToEnumerableOfOne());
            }
        }

        protected internal ConsumptionRequest CreateCanConsumeRequest(object instance, List<Parser> chain, IterationInfo currentInfo, ConsumptionResult canConsumeResult)
        {
            var toBeConsumed = currentInfo.FromNowOn().Take(canConsumeResult.NumConsumed).ToList();
            for(int i = 1; i < toBeConsumed.Count; i++) // start at 1 because the current token will obviously be a valid token for a parser in the chain
            {
                foreach (var parser in chain)
                {
                    var info = currentInfo.Consume(i);
                    var res = parser.CanConsume(instance, info);
                    if(res.Info > info)
                        return new ConsumptionRequest(currentInfo, i);
                }
            }
            return new ConsumptionRequest(currentInfo, canConsumeResult.NumConsumed);
        }

        public virtual List<Parser> GetParserFamily(IContext context, Parser parser)
        {
            var ancestors = context.HierarchyRepository.GetAncestors(parser.Id)
                .Select(x => context.ParserRepository.Get(x));
            var chain = parser.ToEnumerableOfOne().Concat(ancestors).ToList();
            return chain;
        }

        private IList<string> GetCommandIdentifyingSubsequence(string[] args, IContext context)
        {
            var ids = new List<string>();
            ids.Add(RootParserId);
            for (int i = 1; i < args.Length; i++)
            {
                string left = i == 0 ? RootParserId : args[i - 1];
                string right = args[i];
                if (context.HierarchyRepository.IsParent(left, right))
                {
                    ids.Add(right);
                }
            }

            return ids;
        }

        public virtual Parser IdentifyRelevantParser(string[] args, IContext context)
        {
            var ids = GetCommandIdentifyingSubsequence(args, context);
            return context.ParserRepository.Get(ids.Last());
        }
    }
}