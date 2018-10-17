using System;
using System.Collections.Generic;

namespace ArgParser.Core
{
    public class TokenSwitch<T>
    {
        public char? GroupLetter { get; set; }
        public Func<IterationInfo, bool> IsToken { get; set; }
        public Func<IterationInfo, string, int, bool> TakeWhile { get; set; }
        public Action<IterationInfo, T, string[]> Transformer { get; set; }
        public Action<IterationInfo, T, string[], IList<ParsingError>> Validate { get; set; }

    }
}