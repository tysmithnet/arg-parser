using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles.ParseStrategy
{
    public class ParseStrategy : IParseStrategy
    {
        public ParseStrategy(IContext context, string rootParserId)
        {
            Context = context.ThrowIfArgumentNull(nameof(context));
            RootParserId = rootParserId.ThrowIfArgumentNull(nameof(rootParserId));
            SetContext(context);
        }

        private IContext _context;

        public virtual IParseResult Parse(string[] args, IContext context)
        {
            var chainRes = ChainIdentificationStrategy.Identify(new ChainIdentificationRequest
            {
                Args = args,
                Context = context
            });
            var mutatedArgs = ArgsMutator.Mutate(new MutateArgsRequest
            {
                Context = context,
                Args = args,
                Chain = chainRes.Chain
            });
            var info = IterationInfoFactory.Create(new IterationInfoRequest
            {
                ChainIdentificationResult = chainRes,
                MutatedArgs = mutatedArgs,
                OriginalArgs = args
            });
            if (chainRes.IdentifiedParser.FactoryFunction == null)
                throw new NoFactoryFunctionException(
                    $"No factory function set on parser={chainRes.IdentifiedParser.Id}");
            var instance = chainRes.IdentifiedParser.FactoryFunction();
            while (!info.IsComplete())
            {
                var potentialConsumerResult = PotentialConsumerStrategy.IdentifyPotentialConsumer(
                    new PotentialConsumerRequest
                    {
                        ChainIdentificationResult = chainRes,
                        Instance = instance,
                        Info = info
                    });
                if (!potentialConsumerResult.Success)
                    throw new UnexpectedArgException($"todo: change");
                var selected = ConsumerSelectionStrategy.Select(potentialConsumerResult);
                var consumptionRequest = ConsumptionRequestFactory.Create(potentialConsumerResult, selected);
                var consumptionResult = selected.ConsumingParameter.Consume(instance, consumptionRequest);
                if (consumptionResult.ParseExceptions.Any())
                    return new ParseResult(new Dictionary<object, Parser>(), consumptionResult.ParseExceptions);
                info = consumptionResult.Info;
            }

            return ParseResultFactory.Create(new Dictionary<object, Parser>
            {
                [instance] = chainRes.IdentifiedParser
            }, null);
        }

        private void SetContext(IContext context)
        {
            _context = context;
            if (ArgsMutator == null)
                ArgsMutator = new ArgsMutator(context);
            if (ChainIdentificationStrategy == null)
                ChainIdentificationStrategy = new ParserChainIdentificationStrategy(context);
            if (ConsumerSelectionStrategy == null)
                ConsumerSelectionStrategy = new ConsumerSelectionStrategy(context);
            if (ConsumptionRequestFactory == null)
                ConsumptionRequestFactory = new ConsumptionRequestFactory(context);
            if (IterationInfoFactory == null)
                IterationInfoFactory = new IterationInfoFactory(context);
            if (ParseResultFactory == null)
                ParseResultFactory = new ParseResultFactory(context);
            if (PotentialConsumerStrategy == null)
                PotentialConsumerStrategy = new PotentialConsumerStrategy(context);
            ArgsMutator.Context = context;
            ChainIdentificationStrategy.Context = context;
            ConsumerSelectionStrategy.Context = context;
            ConsumptionRequestFactory.Context = context;
            IterationInfoFactory.Context = context;
            ParseResultFactory.Context = context;
            PotentialConsumerStrategy.Context = context;
        }

        public IArgsMutator ArgsMutator { get; set; }
        public IParserChainIdentificationStrategy ChainIdentificationStrategy { get; set; }
        public IConsumerSelectionStrategy ConsumerSelectionStrategy { get; set; }
        public IConsumptionRequestFactory ConsumptionRequestFactory { get; set; }

        public IContext Context
        {
            get => _context;
            set => SetContext(value);
        }

        public IIterationInfoFactory IterationInfoFactory { get; set; }
        public IParseResultFactory ParseResultFactory { get; set; }
        public IPotentialConsumerStrategy PotentialConsumerStrategy { get; set; }
        public string RootParserId { get; protected internal set; }
    }
}