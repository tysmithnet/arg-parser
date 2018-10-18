using System;
using System.Collections.Generic;

namespace ArgParser.Core
{
    public delegate bool TakeWhileCallback(IterationInfo info, string element, int elementNumber);

    public abstract class CommandLineElement<T>
    {
        public delegate void TransformerCallback(IterationInfo info, T instance, string[] takenStrings);

        public TakeWhileCallback TakeWhile { get; set; }
        public TransformerCallback Transformer { get; set; }
    }
}