using System.Collections.Generic;
using System.Linq;
using ArgParser.Core.Extensions;

namespace ArgParser.Core
{
    public static class ContextExtensions
    {
        public static IEnumerable<Parser> PathToRoot(this IContext context, string parserId)
        {
            var ids = parserId.ToEnumerableOfOne().Concat(context.HierarchyRepository.GetAncestors(parserId));
            return ids.Select(i => context.ParserRepository.Get(i)); // todo: error checking
        }
    }
}