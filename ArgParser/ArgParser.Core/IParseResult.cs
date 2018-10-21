using System;

namespace ArgParser.Core
{
    public interface IParseResult
    {
        void When<T>(Action<T> handler);
    }
}