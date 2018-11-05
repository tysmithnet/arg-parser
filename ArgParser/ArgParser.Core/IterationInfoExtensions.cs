using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public static class IterationInfoExtensions
    {
        public static IEnumerable<string> FromNowOn(this IterationInfo info)
        {
            return info.Args.Skip(info.Index);
        }

        public static IEnumerable<string> Rest(this IterationInfo info)
        {
            return info.Args.Skip(info.Index + 1);
        }

        public static string First(this IterationInfo info)
        {
            if(info.Args.Length == 0)
                throw new IndexOutOfRangeException($"Info does not have any args");
            return info.Args.First();
        }

        public static string Last(this IterationInfo info)
        {
            if (info.Args.Length == 0)
                throw new IndexOutOfRangeException($"Info does not have any args");
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

        public static bool IsComplete(this IterationInfo info)
        {
            return info.Index >= info.Args.Length;
        }
    }
}