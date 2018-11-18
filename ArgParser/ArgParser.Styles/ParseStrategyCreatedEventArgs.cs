using System;
using ArgParser.Core;

namespace ArgParser.Styles
{
    public class ParseStrategyCreatedEventArgs : EventArgs
    {
        public ParseStrategy ParseStrategy { get; set; }

        public ParseStrategyCreatedEventArgs(ParseStrategy parseStrategy)
        {
            ParseStrategy = parseStrategy.ThrowIfArgumentNull(nameof(parseStrategy));
        }
    }
}