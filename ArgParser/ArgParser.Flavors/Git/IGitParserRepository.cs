using System.Collections.Generic;

namespace ArgParser.Flavors.Git
{
    public interface IGitParserRepository
    {
        GitParser Create(string name);
        void EstablishParentChildRelationship(string parent, string child);
        GitParser Get(string name);
        IEnumerable<GitParser> GetAncestors(string name);
        IEnumerable<GitParser> GetChildren(string parser, bool recursive);
        GitParser GetParent(string parser);
        bool IsSubCommand(string parserName, string potentialSubCommand);
        GitParser GetSubCommand(string parserName, string subCommand);
    }
}