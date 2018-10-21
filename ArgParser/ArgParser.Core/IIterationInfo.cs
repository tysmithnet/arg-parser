using System.Collections.Generic;

namespace ArgParser.Core
{
    public interface IIterationInfo
    {
        IToken Current { get; }
        IToken Next { get; }
        IReadOnlyList<IToken> Tokens { get; }
        int Index { get; }
        string[] Args { get; }
        IIterationInfo Consume(int numTokens);
    }
}