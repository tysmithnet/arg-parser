using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ArgParser.Core
{
    public static class BasicExtensions
    {
        public static Stream ToStream(this string source)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(source);
            writer.Flush();
            writer.Close();
            stream.Position = 0;
            return stream;
        }

        public static bool IsNullOrWhiteSpace(this string source) => string.IsNullOrWhiteSpace(source);
        public static bool IsNotNullOrWhiteSpace(this string source) => !IsNullOrWhiteSpace(source);

        public static string Join(this IEnumerable<string> strings, string separator) =>
            string.Join(separator, strings);

        public static string JoinNonNullOrWhiteSpace(this IEnumerable<string> strings, string separator)
        {
            return string.Join(separator, strings.Where(x => !IsNullOrWhiteSpace(x)));
        }

        public static IEnumerable<T> PreventNull<T>(this IEnumerable<T> source) => source ?? new T[0];

        public static T ThrowIfArgumentNull<T>(this T thing, string parameterName, string message = null)
        {
            if (thing != null)
                return thing;
            if (message != null)
                throw new ArgumentNullException(parameterName, message);
            throw new ArgumentNullException(parameterName);
        }

        public static IEnumerable<T> ToEnumerableOfOne<T>(this T source) => new[] {source};

        public static Action<object, string[]> ToNonGenericAction<T>(this Action<T, string[]> action)
        {
            return (instance, s) =>
            {
                if (instance is T casted)
                    action(casted, s);
                else
                    throw new ArgumentException(
                        $"Expected to find object of type={typeof(T).FullName}, but found type={instance.GetType().FullName}");
            };
        }

        public static Action<object> ToNonGenericAction<T>(this Action<T> toConvert)
        {
            // todo: does this belong here?
            toConvert.ThrowIfArgumentNull(nameof(toConvert));
            return instance =>
            {
                instance.ThrowIfArgumentNull(nameof(instance));
                if (instance is T casted)
                    toConvert(casted);
                else
                    throw new ArgumentException(
                        $"Expected to find object of type={typeof(T).FullName}, but found type={instance.GetType().FullName}");
            };
        }
    }
}