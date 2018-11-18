using ArgParser.Core;

namespace ArgParser.Styles.ParseStrategy
{
    public interface IConsumptionRequestFactory : IParseStrategyUnit
    {
        ConsumptionRequest Create(PotentialConsumerResult consumerResult, ConsumptionResult selected);
    }
}