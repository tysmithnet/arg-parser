using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public static class BasicExtensions
    {
        public static bool IsNullOrWhiteSpace(this string source) => string.IsNullOrWhiteSpace(source);

        public static string Join(this IEnumerable<string> strings, string separator) =>
            string.Join(separator, strings);

        public static string JoinNonNullOrWhiteSpace(this IEnumerable<string> strings, string separator)
        {
            return string.Join(separator, strings.Where(x => !x.IsNullOrWhiteSpace()));
        }

        public static IEnumerable<T> PreventNull<T>(this IEnumerable<T> source) => source ?? new T[0];

        public static T ThrowIfArgumentNull<T>(this T thing, string parameterName, string message = null)
        {
            if (thing != null)
                return thing;
            if(message != null)
                throw new ArgumentNullException(parameterName, message);
            throw new ArgumentNullException(parameterName);
        }
    }
}