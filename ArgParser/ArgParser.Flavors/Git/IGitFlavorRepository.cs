using System.Collections.Generic;

namespace ArgParser.Flavors.Git
{
    public interface IGitFlavorRepository
    {
        GitFlavor Create(string name);
        void EstablishParentChildRelationship(string parent, string child);
        GitFlavor Get(string name);
        IEnumerable<GitFlavor> GetAncestors(string name);
        IEnumerable<GitFlavor> GetChildren(string flavor, bool recursive);
        GitFlavor GetParent(string flavor);
    }
}