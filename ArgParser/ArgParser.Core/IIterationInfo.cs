using System.Collections.Generic;

namespace ArgParser.Core
{
    public interface IIterationInfo
    {
        bool IsComplete { get; }
        IToken Current { get; }
        IToken Next { get; }
        IReadOnlyList<IToken> Tokens { get; }
        IEnumerable<IToken> Rest { get; }
        int Index { get; }
        string[] Args { get; }
        IIterationInfo Consume(int numTokens);
    }
}