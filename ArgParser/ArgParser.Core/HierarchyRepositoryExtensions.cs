using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public static class HierarchyRepositoryExtensions
    {
        public static IEnumerable<Parser> GetSelfAndAncestors(this IHierarchyRepository repo, string selfId)
        {
            var self = repo.Get(selfId);
            var ancestors = repo.GetAncestors(selfId);
            return new[] {self}.Concat(ancestors);
        }
    }
}