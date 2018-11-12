using System;
using ArgParser.Core;

namespace ArgParser.Styles
{
    public class ParserConsumedEventArgs : ParserEventArgs
    {
        public Parser ConsumingParser { get; protected internal set; }
        public ConsumptionResult ConsumptionResult { get; protected internal set; }

        public ParserConsumedEventArgs(IContext context, Parser consumingParser, ConsumptionResult consumptionResult) :
            base(context)
        {
            ConsumingParser = consumingParser ?? throw new ArgumentNullException(nameof(consumingParser));
            ConsumptionResult = consumptionResult ?? throw new ArgumentNullException(nameof(consumptionResult));
        }
    }
}