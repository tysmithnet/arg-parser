using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles
{
    public class AmbiguousCommandChainException : ParseException
    {
        private static string CreateMessage(IEnumerable<IEnumerable<string>> matchingLists)
        {
            var lists = matchingLists.Select(x => $"[{BasicExtensions.Join(x, " ")}]").Join(", ");
            return $"Ambiguous parse chains found: {lists}";
        }

        public IReadOnlyList<IReadOnlyList<string>> MatchingSequences { get; protected internal set; }
        public AmbiguousCommandChainException(List<List<string>> matchingLists) : base(CreateMessage(matchingLists))
        {
            MatchingSequences = matchingLists.ThrowIfArgumentNull(nameof(matchingLists)).Select(x => x.ToList()).ToList();
        }
    }
}