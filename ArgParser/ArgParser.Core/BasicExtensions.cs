// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="BasicExtensions.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    /// <summary>
    ///     Convenience extensions for types core to .NET
    /// </summary>
    public static class BasicExtensions
    {
        /// <summary>
        ///     Determines whether the provided string is non-null/non-whitespace string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns><c>true</c> if the provided string is non-null/non-whitespace; otherwise, <c>false</c>.</returns>
        public static bool IsNotNullOrWhiteSpace(this string source) => !IsNullOrWhiteSpace(source);

        /// <summary>
        ///     Determines whether the provided string is null or whitespace
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns><c>true</c> if the provided string is null or whitespace; otherwise, <c>false</c>.</returns>
        public static bool IsNullOrWhiteSpace(this string source) => string.IsNullOrWhiteSpace(source);

        /// <summary>
        ///     Joins the provided strings on the supplied separator
        /// </summary>
        /// <param name="strings">The strings.</param>
        /// <param name="separator">The separator.</param>
        /// <returns>The provides strings joined on the supplied separator</returns>
        public static string Join(this IEnumerable<string> strings, string separator) =>
            string.Join(separator, strings);

        /// <summary>
        ///     Joins the provided strings on the supplied separator, but only the non-null/non-whitespace items
        /// </summary>
        /// <param name="strings">The strings.</param>
        /// <param name="separator">The separator.</param>
        /// <returns>Joins the provided strings on the supplied separator, but only the non-null/non-whitespace items</returns>
        public static string JoinNonNullOrWhiteSpace(this IEnumerable<string> strings, string separator)
        {
            return string.Join(separator, strings.Where(x => !IsNullOrWhiteSpace(x)));
        }

        /// <summary>
        ///     Returns an empty enumeration if the supplied enumerable is null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns>An empty enumeration if the supplied enumerable is null</returns>
        public static IEnumerable<T> PreventNull<T>(this IEnumerable<T> source) => source ?? new T[0];

        /// <summary>
        ///     Throws if the supplied argument is null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="thing">The thing.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="message">The message.</param>
        /// <returns>The supplied item if no exception was thrown</returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static T ThrowIfArgumentNull<T>(this T thing, string parameterName, string message = null)
        {
            if (thing != null)
                return thing;
            if (message != null)
                throw new ArgumentNullException(parameterName, message);
            throw new ArgumentNullException(parameterName);
        }

        /// <summary>
        ///     Transform a single item into an enumerable of that item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns>An enumerable of the supplied item</returns>
        public static IEnumerable<T> ToEnumerableOfOne<T>(this T source) => new[] {source};

        /// <summary>
        ///     Transforms a common action pattern to a non-generic version
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">The action.</param>
        /// <param name="strict">if set to <c>true</c> an exception is thrown if the type doesn't match.</param>
        /// <returns>A non generic version of the action</returns>
        public static Action<object, string[]> ToNonGenericAction<T>(this Action<T, string[]> action,
            bool strict = false)
        {
            action.ThrowIfArgumentNull(nameof(action));
            return (instance, s) =>
            {
                if (instance is T casted)
                    action(casted, s);
                else if (strict)
                    throw new ArgumentException(
                        $"Expected to find object of type={typeof(T).FullName}, but found type={instance.GetType().FullName}");
            };
        }

        /// <summary>
        ///     Transforms a common action pattern to a non-generic version
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">The action.</param>
        /// <param name="strict">if set to <c>true</c> an exception is thrown if the type doesn't match.</param>
        /// <returns>A non generic version of the action</returns>
        public static Action<object> ToNonGenericAction<T>(this Action<T> action, bool strict = false)
        {
            action.ThrowIfArgumentNull(nameof(action));
            return instance =>
            {
                instance.ThrowIfArgumentNull(nameof(instance));
                if (instance is T casted)
                    action(casted);
                else if (strict)
                    throw new ArgumentException(
                        $"Expected to find object of type={typeof(T).FullName}, but found type={instance.GetType().FullName}");
            };
        }
    }
}