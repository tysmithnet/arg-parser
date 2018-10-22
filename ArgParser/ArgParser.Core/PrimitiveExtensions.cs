using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArgParser.Core
{
    internal static class PrimitiveExtensions
    {
        public static string Join(this IEnumerable<string> strings, string separator)
        {
            return string.Join(separator, strings);
        }

        public static string JoinNonNullOrWhiteSpace(this IEnumerable<string> strings, string separator)
        {
            return string.Join(separator, strings.Where(x => !x.IsNullOrWhiteSpace()));
        }

        public static bool IsNullOrWhiteSpace(this string source)
        {
            return string.IsNullOrWhiteSpace(source);
        }
    }
}
