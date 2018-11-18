using ArgParser.Core;

namespace ArgParser.Styles.ParseStrategy
{
    public interface IConsumerSelectionStrategy : IParseStrategyUnit
    {
        ConsumptionResult Select(PotentialConsumerResult result);
    }
}