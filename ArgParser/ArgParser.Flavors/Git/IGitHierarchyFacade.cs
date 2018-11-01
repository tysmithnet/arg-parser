using System.Collections.Generic;

namespace ArgParser.Flavors.Git
{
    public interface IGitHierarchyFacade
    {
        GitFlavor GetParent(GitFlavor flavor);
        IEnumerable<GitFlavor> GetChildren(GitFlavor flavor);
        void EstablishParentChildRelationship(GitFlavor parent, GitFlavor child);
    }
}