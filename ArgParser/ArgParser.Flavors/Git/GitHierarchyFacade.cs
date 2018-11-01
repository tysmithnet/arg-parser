using System.Collections.Generic;

namespace ArgParser.Flavors.Git
{
    public class GitHierarchyFacade : IGitHierarchyFacade
    {
        public GitFlavor GetParent(GitFlavor flavor)
        {
            return null;
        }

        public IEnumerable<GitFlavor> GetChildren(GitFlavor flavor)
        {
            return null;
        }

        public void EstablishParentChildRelationship(GitFlavor parent, GitFlavor child)
        {

        }
    }
}