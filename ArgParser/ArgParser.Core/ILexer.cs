using System.Collections.Generic;

namespace ArgParser.Core
{
    public interface ILexer
    {
        IEnumerable<IToken> Lex(string[] args);
    }
}