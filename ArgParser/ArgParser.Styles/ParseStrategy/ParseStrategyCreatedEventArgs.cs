using System;

namespace ArgParser.Styles
{
    public class ParseStrategyCreatedEventArgs : EventArgs
    {
        public ParseStrategy.ParseStrategy ParseStrategy { get; set; }
    }
}