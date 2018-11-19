using System.Collections.Generic;
using ArgParser.Core;

namespace ArgParser.Styles
{
    public class AliasRepository : IAliasRepository
    {
        public string GetAlias(string parserId) =>
            Alias.TryGetValue(parserId, out var registeredAlias) ? registeredAlias : parserId;

        public void SetAlias(string parserId, string alias)
        {
            Alias[parserId] = alias;
        }

        protected internal Dictionary<string, string> Alias { get; set; } = new Dictionary<string, string>();
    }
}