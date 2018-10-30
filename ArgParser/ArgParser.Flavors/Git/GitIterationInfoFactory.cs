using ArgParser.Core;

namespace ArgParser.Flavors.Git
{
    public class GitIterationInfoFactory : DefaultIterationInfoFactory
    {
        /// <inheritdoc />
        public GitIterationInfoFactory()
        {
            Lexer = new GitLexer();
        }
    }
}