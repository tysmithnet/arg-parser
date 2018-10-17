using System;
using System.Collections.Generic;

namespace ArgParser.Core
{
    public abstract class CommandLineElement<T>
    {
        public Func<IterationInfo, string, int, bool> TakeWhile { get; set; }
        public Action<IterationInfo, T, string[]> Transformer { get; set; }
        public Action<string[], T, IList<ParsingError>> Validate { get; set; }
    }
}