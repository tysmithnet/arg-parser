using System.Collections.Generic;

namespace ArgParser.Core
{
    public interface IPositionalStrategy<T> : IParsingStrategy<T>
    {
        IterationInfo Consume(IList<Positional<T>> positionals, T instasnce, IterationInfo info);
        bool IsPositional(IList<Positional<T>> positionals, IterationInfo info);
    }
}