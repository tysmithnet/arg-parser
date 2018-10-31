using ArgParser.Core;

namespace ArgParser.Flavors.Git
{
    public class GitIterationInfoFactory : DefaultIterationInfoFactory
    {
            
        public GitIterationInfoFactory()
        {
            Lexer = new GitLexer();
        }
    }
}