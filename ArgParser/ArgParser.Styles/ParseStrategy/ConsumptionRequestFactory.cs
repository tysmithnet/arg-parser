using ArgParser.Core;

namespace ArgParser.Styles.ParseStrategy
{
    public class ConsumptionRequestFactory : IConsumptionRequestFactory
    {
        public ConsumptionRequestFactory(IContext context)
        {
            Context = context.ThrowIfArgumentNull(nameof(context));
        }

        public ConsumptionRequest Create(PotentialConsumerResult consumerResult, ConsumptionResult selected)
        {
            for (var i = 1;
                i < selected.NumConsumed;
                i++)
                foreach (var parser in consumerResult.Chain)
                {
                    var info = consumerResult.Info.Consume(i);
                    var res = parser.CanConsume(selected, info);
                    if (res.Info > info && res.ConsumingParameter != null && res.ConsumingParameter is Switch)
                        return new ConsumptionRequest(consumerResult.Info, i);
                }

            return new ConsumptionRequest(consumerResult.Info, selected.NumConsumed);
        }

        public IContext Context { get; set; }
    }
}