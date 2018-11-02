using System.Collections.Generic;

namespace ArgParser.Flavors.Git
{
    public interface IGitParserRepository
    {
        bool Contains(string name);
        GitParser Create(string name);
        void EstablishParentChildRelationship(string parent, string child);
        GitParser Get(string name);
        IEnumerable<GitParser> GetAncestors(string name);
        IEnumerable<GitParser> GetChildren(string parser, bool recursive);
        GitParser GetParent(string parser);
        GitParser GetSubCommand(string parserName, string subCommand);
        bool IsSubCommand(string parserName, string potentialSubCommand);
    }
}