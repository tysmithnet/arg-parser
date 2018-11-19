using System.Collections.Generic;

namespace ArgParser.Core
{
    public interface IAliasRepository
    {
        IEnumerable<string> Lookup(string alias);
        string GetAlias(string parserId);
        void SetAlias(string parserId, string alias);
        bool HasAlias(string parserId);
    }
}