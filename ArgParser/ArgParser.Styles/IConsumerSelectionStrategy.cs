using ArgParser.Core;

namespace ArgParser.Styles
{
    public interface IConsumerSelectionStrategy : IParseStrategyUnit
    {
        ConsumptionResult Select(PotentialConsumerResult result);
    }
}