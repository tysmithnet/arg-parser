using System.Collections.Generic;
using System.Text;
using ArgParser.Core;

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

        public IParseResult Parse(string[] args)
        {
            
        }
    }
}
