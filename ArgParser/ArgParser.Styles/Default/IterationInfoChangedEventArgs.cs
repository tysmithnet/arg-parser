using System;
using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class IterationInfoChangedEventArgs : ParserEventArgs
    {
        public IterationInfo Previous { get; protected internal set; }
        public IterationInfo Changed { get; protected internal set; }

        public IterationInfoChangedEventArgs(IContext context, IterationInfo previous, IterationInfo changed) :
            base(context)
        {
            Previous = previous;
            Changed = changed ?? throw new ArgumentNullException(nameof(changed));
        }
    }
}