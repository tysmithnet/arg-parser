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

        public static IEnumerable<string> Rest(this IterationInfo info)
        {
            return info.Args.Skip(info.Index + 1);
        }

        public static string First(this IterationInfo info)
        {
            return info.Args.First();
        }

        public static string Last(this IterationInfo info)
        {
            return info.Args.Last();
        }

        public static string Next(this IterationInfo info)
        {
            return info.Args[info.Index + 1];
        }

        public static bool HasNext(this IterationInfo info)
        {
            return info.Index + 1 < info.Args.Length;
        }

        public static string Current(this IterationInfo info)
        {
            return info.Args[info.Index];
        }
    }
}