using ArgParser.Core;

namespace ArgParser.Styles
{
    public interface IConsumerSelectionStrategy
    {
        ConsumptionResult Select(PotentialConsumerResult result);
    }
}