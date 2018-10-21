using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public class DefaultLexer : ILexer
    {
        /// <inheritdoc />
        public IEnumerable<IToken> Lex(string[] args)
        {
            return args?.Select(a => new Token(a)).ToList() ?? new List<Token>();
        }
    }
}