using System;
using System.Collections.Generic;

namespace ArgParser.Core
{
    public class Switch<T> : CommandLineElement<T>
    {
        public char? GroupLetter { get; set; }
        public Func<IterationInfo, bool> IsToken { get; set; }
    }
}