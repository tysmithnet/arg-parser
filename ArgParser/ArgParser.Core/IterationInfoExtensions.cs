using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public static class IterationInfoExtensions
    {
        public static string First(this IterationInfo info)
        {
            if (info.Args.Length == 0)
                throw new IndexOutOfRangeException($"Info does not have any args");
            return info.Args.First();
        }

        public static IEnumerable<string> FromNowOn(this IterationInfo info) => info.Args.Skip(info.Index);

        public static bool HasNext(this IterationInfo info) => info.Index + 1 < info.Args.Length;

        public static bool IsComplete(this IterationInfo info) => info.Index >= info.Args.Length;

        public static string Last(this IterationInfo info)
        {
            if (info.Args.Length == 0)
                throw new IndexOutOfRangeException($"Info does not have any args");
            return info.Args.Last();
        }

        public static string Next(this IterationInfo info) => info.Args[info.Index + 1];

        public static IEnumerable<string> Rest(this IterationInfo info) => info.Args.Skip(info.Index + 1);
    }
}