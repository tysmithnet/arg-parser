using System;
using System.Collections.Generic;
using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class PotentialConsumersIdentified : ParserEventArgs
    {
        public IEnumerable<ConsumptionResult> ConsumptionResults { get; protected internal set; }

        public PotentialConsumersIdentified(IContext context, IEnumerable<ConsumptionResult> consumptionResults) :
            base(context)
        {
            ConsumptionResults = consumptionResults ?? throw new ArgumentNullException(nameof(consumptionResults));
        }
    }
}