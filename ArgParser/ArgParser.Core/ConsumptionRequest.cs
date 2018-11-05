using System;

namespace ArgParser.Core
{
    public class ConsumptionRequest
    {
        public ConsumptionRequest(IterationInfo info, int max = 1)
        {
            Info = info ?? throw new ArgumentNullException(nameof(info));
            Max = max;
        }

        public IterationInfo Info { get; protected internal set; }
        public int Max { get; protected internal set; }
    }
}