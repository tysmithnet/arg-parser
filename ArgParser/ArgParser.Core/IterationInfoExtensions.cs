using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public static class IterationInfoExtensions
    {
        public static IEnumerable<string> FromNowOwn(this IterationInfo info)
        {
            return info.Args.Skip(info.Index);
        }

        public static string Current(this IterationInfo info)
        {
            return info.Args[info.Index];
        }
    }
}