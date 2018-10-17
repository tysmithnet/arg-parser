using System.Collections.Generic;

namespace ArgParser.Core
{
    public interface IPositionalStrategy<T> : IParsingStrategy<T>
    {
        IterationInfo Consume(IList<Positional<T>> positionals, T instance, IterationInfo info, IPositionalStrategy<T> parent = null);
        bool IsPositional(IList<Positional<T>> positionals, IterationInfo info, IPositionalStrategy<T> parent = null);
    }
}