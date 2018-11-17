using ArgParser.Core;

namespace ArgParser.Styles
{
    public interface IConsumptionRequestFactory
    {
        ConsumptionRequest Create(PotentialConsumerResult consumerResult, ConsumptionResult selected);
    }
}