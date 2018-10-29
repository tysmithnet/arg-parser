using System.Text.RegularExpressions;
using ArgParser.Core;

namespace ArgParser.Flavors
{
    public class GitToken : IToken
    {
        public Match GroupMatch { get; set; }

        public bool IsAnyMatch =>
            WordMatch.Success || LetterMatch.Success || WordEqualMatch.Success || GroupMatch.Success;

        public string Key => (WordEqualMatch?.Success ?? false) ? WordMatch?.Groups["k"].Value : null;
        public char? Letter => (LetterMatch?.Success ?? false) ? LetterMatch?.Groups["k"].Value[0] : null;
        public Match LetterMatch { get; set; }
        public int Order { get; set; }

        /// <inheritdoc />
        public string Raw { get; set; }

        public string Value => (WordEqualMatch?.Success ?? false) ? WordMatch?.Groups["v"].Value : null;
        public string Word => (WordMatch?.Success ?? false) ? WordMatch?.Groups["k"].Value : null;
        public Match WordEqualMatch { get; set; }
        public Match WordMatch { get; set; }
    }
}