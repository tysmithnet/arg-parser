using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Flavors
{
    public class GitLexer : ILexer
    {
        /// <inheritdoc />
        public IEnumerable<IToken> Lex(string[] args)
        {
            return DefaultLexer.Lex(args).Select(x => TokenExtensions.ToGitToken(x));
        }

        public DefaultLexer DefaultLexer { get; set; } = new DefaultLexer();
    }
}