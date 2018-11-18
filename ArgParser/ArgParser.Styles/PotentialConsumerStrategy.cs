using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles
{
    public class PotentialConsumerStrategy : IPotentialConsumerStrategy
    {
        public PotentialConsumerStrategy(IContext context)
        {
            Context = context.ThrowIfArgumentNull(nameof(context));
        }

        public IContext Context { get; set; }

        public PotentialConsumerResult IdentifyPotentialConsumer(PotentialConsumerRequest request)
        {
            var consumptionResultsForTheParsersWhoCanConsume = request.ChainIdentificationResult.Chain
                .Select(x => x.CanConsume(request.Instance, request.Info))
                .Where(x => x.NumConsumed > 0).ToList();
            return new PotentialConsumerResult()
            {
                ConsumptionResults = consumptionResultsForTheParsersWhoCanConsume,
                Chain = request.ChainIdentificationResult.Chain,
                Info = request.Info
            };
        }
    }
}