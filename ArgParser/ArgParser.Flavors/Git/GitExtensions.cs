using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Flavors.Git
{
    public static class GitExtensions
    {
        public static IEnumerable<IToken> FromNowOn(this IIterationInfo info) => new[] {info.Current}.Concat(info.Rest);
        public static readonly GitIterationInfoFactory Factory = new GitIterationInfoFactory();
        public static GitToken ToGitToken(this IToken token)
        {
            if (token is GitToken casted)
                return casted;
            return new GitToken(token.Raw);
        }

        public static GitIterationInfo ToGitIterationInfo(this IIterationInfo info)
        {
            if (info is GitIterationInfo casted)
                return casted;
            var newGuy = (GitIterationInfo)Factory.Create(info.Args);
            newGuy.Index = info.Index;
            return newGuy;
        }
    }
}