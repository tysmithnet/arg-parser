using System.Collections.Generic;

namespace ArgParser.Flavors.Git
{
    public interface IGitFlavorRepository
    {
        GitParser Create(string name);
        void EstablishParentChildRelationship(string parent, string child);
        GitParser Get(string name);
        IEnumerable<GitParser> GetAncestors(string name);
        IEnumerable<GitParser> GetChildren(string flavor, bool recursive);
        GitParser GetParent(string flavor);
    }
}