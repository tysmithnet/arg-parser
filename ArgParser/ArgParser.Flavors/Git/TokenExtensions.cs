using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Flavors.Git
{
    public static class TokenExtensions
    {
        public static IEnumerable<IToken> FromNowOn(this IIterationInfo info) => new[] {info.Current}.Concat(info.Rest);

        public static GitToken ToGitToken(this IToken token)
        {
            if (token is GitToken casted)
                return casted;
            return new GitToken(token.Raw);
        }
    }
}