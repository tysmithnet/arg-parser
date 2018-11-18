using ArgParser.Core;

namespace ArgParser.Styles
{
    public interface IConsumptionRequestFactory : IParseStrategyUnit
    {
        ConsumptionRequest Create(PotentialConsumerResult consumerResult, ConsumptionResult selected);
    }
}