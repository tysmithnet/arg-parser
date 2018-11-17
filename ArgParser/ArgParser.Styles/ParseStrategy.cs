using System.Collections.Generic;
using ArgParser.Core;

namespace ArgParser.Styles
{
    public class ParseStrategy : IParseStrategy
    {
        public ParseStrategy(string rootParserId)
        {
            RootParserId = rootParserId.ThrowIfArgumentNull(nameof(rootParserId));
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

        public IArgsMutator ArgsMutator { get; set; } = new ArgsMutator();

        public IParserChainIdentificationStrategy ChainIdentificationStrategy { get; set; } =
            new ParserChainIdentificationStrategy();

        public IConsumerSelectionStrategy ConsumerSelectionStrategy { get; set; }
        public IConsumptionRequestFactory ConsumptionRequestFactory { get; set; } = new ConsumptionRequestFactory();
        public IIterationInfoFactory IterationInfoFactory { get; set; } = new IterationInfoFactory();
        public IParseResultFactory ParseResultFactory { get; set; } = new ParseResultFactory();
        public IPotentialConsumerStrategy PotentialConsumerStrategy { get; set; } = new PotentialConsumerStrategy();

        public string RootParserId { get; protected internal set; }
    }
}