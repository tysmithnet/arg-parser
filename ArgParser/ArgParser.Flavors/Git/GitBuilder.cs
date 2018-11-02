using System.Collections.Generic;
using System.Text;

namespace ArgParser.Flavors.Git
{
    public class GitBuilder
    {
        public IGitContext GitContext { get; set; } = new GitContext();
        
        public ParserBuilder AddParser(string name)
        {
            return new ParserBuilder(name, this, GitContext);       
        }

        public GitBuilder AddSubCommand(string parent, string child)
        {
            GitContext.GitParserRepository.EstablishParentChildRelationship(parent, child);
            return this;
        }
    }
}
