using System.Text.RegularExpressions;
using ArgParser.Core;

namespace ArgParser.Flavors
{
    public static class TokenExtensions
    {
        public static GitToken ToGitToken(this IToken token)
        {
            if (token is GitToken casted)
                return casted;
            return new GitToken
            {
                Raw = token.Raw,
                WordMatch = Regex.Match(token.Raw, "^--(?<k>[^-]+)$"),
                LetterMatch = Regex.Match(token.Raw, "^-(?<k>[^-])$"),
                GroupMatch = Regex.Match(token.Raw, @"^-(?<k>\S+)$"),
                WordEqualMatch = Regex.Match(token.Raw, @"^(?<k>--[^-]+)=(?<v>\S+)$")
            };
        }
    }
}