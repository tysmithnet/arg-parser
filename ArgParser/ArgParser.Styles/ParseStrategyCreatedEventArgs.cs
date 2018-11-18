using System;

namespace ArgParser.Styles
{
    public class ParseStrategyCreatedEventArgs : EventArgs
    {
        public ParseStrategy ParseStrategy { get; set; }
    }
}