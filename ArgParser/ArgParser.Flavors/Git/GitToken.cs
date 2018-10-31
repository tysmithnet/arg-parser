using System;
using System.Text.RegularExpressions;
using ArgParser.Core;

namespace ArgParser.Flavors.Git
{
    public class GitToken : IToken
    {
        /// <inheritdoc />
        public GitToken(string raw)
        {
            Raw = raw ?? throw new ArgumentNullException(nameof(raw));
            WordMatch = Regex.Match(Raw, "^--(?<k>[^-]+)$");
            LetterMatch = Regex.Match(Raw, "^-(?<k>[^-])$");
            GroupMatch = Regex.Match(Raw, @"^-(?<k>\S+)$");
            WordEqualMatch = Regex.Match(Raw, @"^--(?<k>[^-]+)=(?<v>\S+)$");
        }

        public Match GroupMatch { get; set; }

        public bool IsAnyMatch =>
            WordMatch.Success || LetterMatch.Success || WordEqualMatch.Success || GroupMatch.Success;

        public string Key => (WordEqualMatch?.Success ?? false) ? WordEqualMatch?.Groups["k"].Value : null;
        public char? Letter => (LetterMatch?.Success ?? false) ? LetterMatch?.Groups["k"].Value[0] : null;
        public Match LetterMatch { get; set; }
        public int Order { get; set; }
        public string Raw { get; set; }
        public string Value => (WordEqualMatch?.Success ?? false) ? WordEqualMatch?.Groups["v"].Value : null;
        public string Word => (WordMatch?.Success ?? false) ? WordMatch?.Groups["k"].Value : null;
        public Match WordEqualMatch { get; set; }
        public Match WordMatch { get; set; }
    }
}