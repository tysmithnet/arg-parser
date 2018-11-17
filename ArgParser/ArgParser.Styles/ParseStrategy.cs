using System.Collections.Generic;
using ArgParser.Core;

namespace ArgParser.Styles
{
    public class ParseStrategy : IParseStrategy
    {
        public IContext Context { get; set; }
        public ParseStrategy(IContext context, string rootParserId)
        {
            Context = context.ThrowIfArgumentNull(nameof(context));
            RootParserId = rootParserId.ThrowIfArgumentNull(nameof(rootParserId));
            ArgsMutator = new ArgsMutator(context);
            ChainIdentificationStrategy = new ParserChainIdentificationStrategy(context);
            ConsumerSelectionStrategy = new ConsumerSelectionStrategy(context);
            ConsumptionRequestFactory = new ConsumptionRequestFactory(context);
            IterationInfoFactory = new IterationInfoFactory(context);
            ParseResultFactory = new ParseResultFactory(context);
            PotentialConsumerStrategy = new PotentialConsumerStrategy(context);
        }

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
            var instance = chainRes.IdentifiedParser.FactoryFunction();
            while (!info.IsComplete())
            {
                var potentialConsumerResult = PotentialConsumerStrategy.IdentifyPotentialConsumer(
                    new PotentialConsumerRequest
                    {
                        ChainIdentificationResult = chainRes
                    });
                if (!potentialConsumerResult.Success)
                    throw new UnexpectedArgException($"todo: change");
                var selected = ConsumerSelectionStrategy.Select(potentialConsumerResult);
                var consumptionRequest = ConsumptionRequestFactory.Create(potentialConsumerResult, selected);
                var consumptionResult = selected.ConsumingParameter.Consume(instance, consumptionRequest);
                info = consumptionResult.Info;
            }

            return ParseResultFactory.Create(new Dictionary<object, Parser>
            {
                [instance] = chainRes.IdentifiedParser
            }, null);
        }

        public IArgsMutator ArgsMutator { get; set; }
        public IParserChainIdentificationStrategy ChainIdentificationStrategy { get; set; }
        public IConsumerSelectionStrategy ConsumerSelectionStrategy { get; set; }
        public IConsumptionRequestFactory ConsumptionRequestFactory { get; set; }
        public IIterationInfoFactory IterationInfoFactory { get; set; }
        public IParseResultFactory ParseResultFactory { get; set; }
        public IPotentialConsumerStrategy PotentialConsumerStrategy { get; set; }

        public string RootParserId { get; protected internal set; }
    }
}