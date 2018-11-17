using System.Linq;

namespace ArgParser.Styles
{
    public class PotentialConsumerStrategy : IPotentialConsumerStrategy
    {
        public PotentialConsumerResult IdentifyPotentialConsumer(PotentialConsumerRequest request)
        {
            var consumptionResultsForTheParsersWhoCanConsume = request.ChainIdentificationResult.Chain
                .Select(x => x.CanConsume(request.Instance, request.Info))
                .Where(x => x.NumConsumed > 0).ToList();
            return new PotentialConsumerResult()
            {
                ConsumptionResults = consumptionResultsForTheParsersWhoCanConsume
            };
        }
    }
}