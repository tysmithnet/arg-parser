using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public abstract class ParserEventArgs : EventArgs
    {
        public IContext Context { get; protected internal set; }

        protected ParserEventArgs(IContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }

    public class ParserIdentifedEventArgs : ParserEventArgs
    {
        public Parser Parser { get; protected internal set; }

        public ParserIdentifedEventArgs(Parser parser, IContext context) : base(context)
        {
            Parser = parser.ThrowIfArgumentNull(nameof(parser));
            Context = context.ThrowIfArgumentNull(nameof(context));
        }
    }

    public class PotentialConsumersIdentified : ParserEventArgs
    {
        public IEnumerable<ConsumptionResult> ConsumptionResults { get; protected internal set; }

        public PotentialConsumersIdentified(IContext context, IEnumerable<ConsumptionResult> consumptionResults) :
            base(context)
        {
            ConsumptionResults = consumptionResults ?? throw new ArgumentNullException(nameof(consumptionResults));
        }
    }

    public class ParserCollectionIdentifiedEventArgs : ParserEventArgs
    {
        public IEnumerable<Parser> Parsers { get; protected internal set; }

        public ParserCollectionIdentifiedEventArgs(IEnumerable<Parser> parsers, IContext context) : base(context)
        {
            Parsers = parsers.ThrowIfArgumentNull(nameof(parsers));
            Context = context.ThrowIfArgumentNull(nameof(context));
        }
    }
    
    public class ArgsMutatedEventArgs : ParserEventArgs
    {
        public string[] Original { get; protected internal set; }
        public string[] Mutated { get; protected internal set; }

        public ArgsMutatedEventArgs(string[] original, string[] mutated, IContext context) : base(context)
        {
            Original = original.ThrowIfArgumentNull(nameof(original));
            Mutated = mutated.ThrowIfArgumentNull(nameof(mutated));
            Context = context.ThrowIfArgumentNull(nameof(context));
        }
    }

    public class InstanceCreatedEventArgs : ParserEventArgs
    {
        public object Instance { get; protected internal set; }
        public Parser Creator { get; protected internal set; }
        public InstanceCreatedEventArgs(object instance, Parser creator, IContext context) : base(context) // todo: get rid of context here somehow, and for alll of them
        {
            Instance = instance ?? throw new ArgumentNullException(nameof(instance));
            Creator = creator.ThrowIfArgumentNull(nameof(creator));
            Context = context.ThrowIfArgumentNull(nameof(context));
        }
    }

    public class IterationInfoChangedEventArgs : ParserEventArgs
    {
        public IterationInfo Previous { get; protected internal set; }
        public IterationInfo Changed { get; protected internal set; }

        public IterationInfoChangedEventArgs(IContext context, IterationInfo previous, IterationInfo changed) :
            base(context)
        {
            Previous = previous;
            Changed = changed ?? throw new ArgumentNullException(nameof(changed));
        }
    }

    public class ConsumptionRequestCreatedEventArgs : ParserEventArgs
    {
        public ConsumptionRequest ConsumptionRequest { get; protected internal set; }
        public Parser ConsumingParser { get; protected internal set; }

        public ConsumptionRequestCreatedEventArgs(IContext context, ConsumptionRequest consumptionRequest,
            Parser consumingParser) : base(context)
        {
            ConsumptionRequest = consumptionRequest ?? throw new ArgumentNullException(nameof(consumptionRequest));
            ConsumingParser = consumingParser ?? throw new ArgumentNullException(nameof(consumingParser));
        }
    }

    public class ParserConsumedEventArgs : ParserEventArgs
    {
        public Parser ConsumingParser { get; protected internal set; }
        public ConsumptionResult ConsumptionResult { get; protected internal set; }

        public ParserConsumedEventArgs(IContext context, Parser consumingParser, ConsumptionResult consumptionResult) :
            base(context)
        {
            ConsumingParser = consumingParser ?? throw new ArgumentNullException(nameof(consumingParser));
            ConsumptionResult = consumptionResult ?? throw new ArgumentNullException(nameof(consumptionResult));
        }
    }

    public class ParseStrategy : IParseStrategy
    {
        public event EventHandler<ParserIdentifedEventArgs> ParserIdentified;
        public event EventHandler<ParserCollectionIdentifiedEventArgs> ParserCollectionIdentified;
        public event EventHandler<ArgsMutatedEventArgs> ArgsMutated;
        public event EventHandler<InstanceCreatedEventArgs> InstanceCreated;
        public event EventHandler<IterationInfoChangedEventArgs> IterationInfoChanged;
        public event EventHandler<PotentialConsumersIdentified> PotentialConsumersIdentified;
        public event EventHandler<ConsumptionRequestCreatedEventArgs> ConsumptionRequestCreated;
        public event EventHandler<ParserConsumedEventArgs> ParserConsumed; 

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
            OnParserIdentified(new ParserIdentifedEventArgs(parser, context));
            var chain = GetParserFamily(context, parser);
            OnParserCollectionIdentified(new ParserCollectionIdentifiedEventArgs(chain, context));
            var newArgs = MutateArgs(args, chain, context);
            OnArgsMutated(new ArgsMutatedEventArgs(args.ToArray(), newArgs.ToArray(), context));
            args = newArgs;
            var subcommandSequence = GetCommandIdentifyingSubsequence(args, context);
            if (parser.FactoryFunction == null)
                throw new NoFactoryFunctionException($"No factory function on parser={parser.Id}");
            var instance = parser.FactoryFunction();
            OnInstanceCreated(new InstanceCreatedEventArgs(instance, parser, context));
            var info = new IterationInfo(args, subcommandSequence.Count);
            OnIterationInfoChanged(new IterationInfoChangedEventArgs(context, null, info));
            try
            {
                while (!info.IsComplete())
                {
                    var consumptionResultsForTheParsersWhoCanConsume = chain
                        .Select(x => x.CanConsume(instance, info))
                        .Where(x => x.NumConsumed > 0).ToList();
                    if (!consumptionResultsForTheParsersWhoCanConsume.Any())
                        throw new UnexpectedArgException(
                            $"Encountered an argument that could not be parsed. Argument={info.Current}, Parsers={chain.Select(p => p.Id).Join(", ")}");
                    OnPotentialConsumersIdentified(new PotentialConsumersIdentified(context, consumptionResultsForTheParsersWhoCanConsume));
                    var whoWillConsume = IdentifyParserToConsume(chain, consumptionResultsForTheParsersWhoCanConsume);
                    var requestThatLimitsConsumption = CreateCanConsumeRequest(instance, chain, info,
                        consumptionResultsForTheParsersWhoCanConsume.ToList());
                    OnConsumptionRequestCreated(new ConsumptionRequestCreatedEventArgs(context, requestThatLimitsConsumption, whoWillConsume));
                    var consumptionResult = whoWillConsume.Consume(instance, requestThatLimitsConsumption);
                    OnParserConsumed(new ParserConsumedEventArgs(context, whoWillConsume, consumptionResult));
                    var copy = info;
                    info = consumptionResult.Info;
                    OnIterationInfoChanged(new IterationInfoChangedEventArgs(context, copy, info));
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

        protected internal virtual string[] MutateArgs(string[] args, List<Parser> chain, IContext context)
        {
            var allSwitchesForChain = chain.SelectMany(x => x.Parameters).OfType<Switch>().ToList();
            var booleanSwitches = allSwitchesForChain.OfType<BooleanSwitch>().ToList();
            var others = allSwitchesForChain.Except(booleanSwitches).ToList();
            var booleanLetters = booleanSwitches.Where(s => s.Letter.HasValue).Select(x => x.Letter.Value.ToString()).Join("");
            var otherLetters = others.Where(s => s.Letter.HasValue).Select(x => x.Letter.Value.ToString()).Join("");
            if (!booleanLetters.Any())
                return args;
            List<string> groups;
            if(others.Any())
                groups = args.Where(a => Regex.IsMatch(a, $"-[{booleanLetters}]+[{otherLetters}]?")).ToList();
            else
                groups = args.Where(a => Regex.IsMatch(a, $"-[{booleanLetters}]+")).ToList();
            var copy = args.ToList();
            foreach (var g in groups)
            {
                for (var i = 0; i < copy.Count; i++)
                {
                    var c = copy[i];
                    if (g.Contains(c))
                    {
                        copy.RemoveAt(i);
                        var letters = c.Substring(1).ToCharArray().Reverse();
                        foreach (var letter in letters)
                        {
                            copy.Insert(i, $"-{letter}");
                        }
                    }
                }
            }

            return copy.ToArray();
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

        protected virtual void OnParserIdentified(ParserIdentifedEventArgs e)
        {
            ParserIdentified?.Invoke(this, e);
        }

        protected virtual void OnParserCollectionIdentified(ParserCollectionIdentifiedEventArgs e)
        {
            ParserCollectionIdentified?.Invoke(this, e);
        }

        protected virtual void OnArgsMutated(ArgsMutatedEventArgs e)
        {
            ArgsMutated?.Invoke(this, e);
        }

        protected virtual void OnInstanceCreated(InstanceCreatedEventArgs e)
        {
            InstanceCreated?.Invoke(this, e);
        }

        protected virtual void OnIterationInfoChanged(IterationInfoChangedEventArgs e)
        {
            IterationInfoChanged?.Invoke(this, e);
        }

        protected virtual void OnPotentialConsumersIdentified(PotentialConsumersIdentified e)
        {
            PotentialConsumersIdentified?.Invoke(this, e);
        }

        protected virtual void OnConsumptionRequestCreated(ConsumptionRequestCreatedEventArgs e)
        {
            ConsumptionRequestCreated?.Invoke(this, e);
        }

        protected virtual void OnParserConsumed(ParserConsumedEventArgs e)
        {
            ParserConsumed?.Invoke(this, e);
        }
    }
}
