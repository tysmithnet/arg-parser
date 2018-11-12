using System;
using ArgParser.Core;

namespace ArgParser.Styles
{
    public class ConsumptionRequestCreatedEventArgs : ParserEventArgs
    {
        public ConsumptionRequest ConsumptionRequest { get; protected internal set; }
        public Parser ConsumingParser { get; protected internal set; }

        public ConsumptionRequestCreatedEventArgs(IContext context, ConsumptionRequest consumptionRequest,
            Parser consumingParser) : base(context)
        {
            ConsumptionRequest = consumptionRequest ?? throw new ArgumentNullException(nameof(consumptionRequest));
            ConsumingParser = consumingParser ?? throw new ArgumentNullException(nameof(consumingParser));
        }
    }
}