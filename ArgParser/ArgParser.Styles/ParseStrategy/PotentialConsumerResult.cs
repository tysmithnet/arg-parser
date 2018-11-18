using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles.ParseStrategy
{
    public class PotentialConsumerResult
    {
        public IEnumerable<Parser> Chain { get; set; }
        public IEnumerable<ConsumptionResult> ConsumptionResults { get; set; }
        public IterationInfo Info { get; set; }
        public bool Success => ConsumptionResults.Any(x => x.NumConsumed > 0);
    }
}