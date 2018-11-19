using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles
{
    public class AliasRepository : IAliasRepository
    {
        public IEnumerable<string> Lookup(string alias)
        {
            return Aliases.Where(kvp => kvp.Value == alias).Select(x => x.Key);
        }

        public string GetAlias(string parserId) => Aliases[parserId];

        public void SetAlias(string parserId, string alias)
        {
            Aliases[parserId] = alias;
        }

        public bool HasAlias(string parserId)
        {
            return Aliases.ContainsKey(parserId);
        }

        protected internal Dictionary<string, string> Aliases { get; set; } = new Dictionary<string, string>();
    }
}