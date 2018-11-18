using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles
{
    public class PotentialConsumerResult
    {
        public IList<Parser> Chain { get; set; }
        public IList<ConsumptionResult> ConsumptionResults { get; set; }
        public IterationInfo Info { get; set; }
        public bool Success => ConsumptionResults.Any(x => x.NumConsumed> 0);

        public PotentialConsumerResult(IEnumerable<Parser> chain, IEnumerable<ConsumptionResult> consumptionResults,
            IterationInfo info)
        {
            Chain = chain.ThrowIfArgumentNull(nameof(chain)).ToList();
            ConsumptionResults = consumptionResults.ThrowIfArgumentNull(nameof(consumptionResults)).ToList();
            Info = info.ThrowIfArgumentNull(nameof(info));
        }
    }
}