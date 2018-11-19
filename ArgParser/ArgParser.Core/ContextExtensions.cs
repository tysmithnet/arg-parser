using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public static class ContextExtensions
    {
        public static IEnumerable<Parser> PathToRoot(this IContext context, string parserId)
        {
            var ids = parserId.ToEnumerableOfOne().Concat(context.HierarchyRepository.GetAncestors(parserId));
            return ids.Select(i => context.ParserRepository.Get(i)); // todo: error checking
        }

        public static Parser RootParser(this IContext context)
        {
            var rootId = context.ThrowIfArgumentNull(nameof(context)).HierarchyRepository.GetRoot();
            return context.ParserRepository.Get(rootId);
        }
    }
}