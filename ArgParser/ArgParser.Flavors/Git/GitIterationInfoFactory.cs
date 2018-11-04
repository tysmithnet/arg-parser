using System.Linq;
using ArgParser.Core;

namespace ArgParser.Flavors.Git
{
    public class GitIterationInfoFactory : IIterationInfoFactory
    {
        public IIterationInfo Create(string[] args)
        {
            var lexer = new GitLexer();
            var tokens = lexer.Lex(args).Cast<GitToken>();
            return new GitIterationInfo(args, tokens);
        }
    }
}