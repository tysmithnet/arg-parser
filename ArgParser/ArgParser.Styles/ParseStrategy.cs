using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles
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

        public virtual IParseResult Parse(string[] args)
        {
            ChainIdentificationResult chainRes = null;
            try
            {
                chainRes = ChainIdentificationStrategy.Identify(new ChainIdentificationRequest(args, Context));
                var mutatedArgs = ArgsMutator.Mutate(new MutateArgsRequest(args, chainRes.Chain, Context));
                var info = IterationInfoFactory.Create(new IterationInfoRequest(chainRes, mutatedArgs, args));
                if (chainRes.IdentifiedParser.FactoryFunction == null)
                    throw new NoFactoryFunctionException(chainRes.IdentifiedParser);
                var instance = chainRes.IdentifiedParser.FactoryFunction();
                while (!info.IsComplete())
                {
                    var potentialConsumerRequest = new PotentialConsumerRequest(chainRes, info, instance);
                    var potentialConsumerResult = PotentialConsumerStrategy.IdentifyPotentialConsumer(potentialConsumerRequest);
                    if (!potentialConsumerResult.Success)
                        throw new UnexpectedArgException(info);
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
            catch (ParseException e)
            {
                return new ParseResult(null, e.ToEnumerableOfOne());
            }
            finally
            {
                chainRes?.Chain.ToList().ForEach(p => p.Reset());
            }
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