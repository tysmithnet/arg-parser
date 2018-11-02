using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Flavors.Git
{
    public class GitLexer : ILexer
    {
        public IEnumerable<IToken> Lex(string[] args)
        {
            return DefaultLexer.Lex(args).Select(x => x.ToGitToken());
        }

        public DefaultLexer DefaultLexer { get; set; } = new DefaultLexer();
    }
}