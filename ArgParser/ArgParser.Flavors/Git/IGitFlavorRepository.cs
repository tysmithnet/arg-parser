using System.Collections.Generic;

namespace ArgParser.Flavors.Git
{
    public interface IGitFlavorRepository
    {
        GitFlavor Create(string name);
        GitFlavor Get(string name);
        GitFlavor GetParent(string flavor, bool recursive);
        IEnumerable<GitFlavor> GetChildren(string flavor, bool recursive);
        void EstablishParentChildRelationship(string parent, string child);
    }
}