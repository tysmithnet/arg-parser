using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles
{
    public class ConsumerSelectionStrategy : IConsumerSelectionStrategy
    {
        public ConsumerSelectionStrategy(IContext context)
        {
            Context = context.ThrowIfArgumentNull(nameof(context));
        }

        public ConsumptionResult Select(PotentialConsumerResult result)
        {
            ConsumptionResult foundResult = null;
            var switchResults = result.ConsumptionResults.Where(r => r.ConsumingParameter is Switch).ToList();
            if (switchResults.Any())
                foundResult = switchResults.FirstOrDefault(r => result.Chain.Contains(r.ConsumingParameter.Parser));

            if (foundResult == null)
                foundResult =
                    result.ConsumptionResults.FirstOrDefault(r => result.Chain.Contains(r.ConsumingParameter.Parser));

            if (foundResult == null)
                throw new ForwardProgressException($"Expected to find a parser to consume, but found none");
            return foundResult;
        }

        public IContext Context { get; set; }
    }

}