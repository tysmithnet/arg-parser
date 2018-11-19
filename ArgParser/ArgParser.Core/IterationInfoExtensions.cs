// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 11-04-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="IterationInfoExtensions.cs" company="ArgParser.Core">
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
    ///     Convenience extensions for IterationInfo
    /// </summary>
    public static class IterationInfoExtensions
    {
        /// <summary>
        ///     Returns the argument in the sequence
        /// </summary>
        /// <param name="info">The information.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Args</exception>
        public static string First(this IterationInfo info)
        {
            if (info.Args.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(info.Args), $"Info does not have any args");
            return info.Args.First();
        }

        /// <summary>
        ///     Returns the arguments at the readhead and beyond
        /// </summary>
        /// <param name="info">The information.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        public static IEnumerable<string> FromNowOn(this IterationInfo info) => info.Args.Skip(info.Index);

        /// <summary>
        ///     Determines whether there is another argument after the read head
        /// </summary>
        /// <param name="info">The information.</param>
        /// <returns><c>true</c> if the specified information has next; otherwise, <c>false</c>.</returns>
        public static bool HasNext(this IterationInfo info) => info.Index + 1 < info.Args.Length;

        /// <summary>
        ///     Determines whether the specified information has reached the end
        /// </summary>
        /// <param name="info">The information.</param>
        /// <returns><c>true</c> if the specified information is complete; otherwise, <c>false</c>.</returns>
        public static bool IsComplete(this IterationInfo info) => info.Index >= info.Args.Length;

        /// <summary>
        ///     Returns the last arg in the sequence
        /// </summary>
        /// <param name="info">The information.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Args</exception>
        public static string Last(this IterationInfo info)
        {
            if (info.Args.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(info.Args), $"Info does not have any args");
            return info.Args.Last();
        }

        /// <summary>
        ///     Returns the next arg after the read head
        /// </summary>
        /// <param name="info">The information.</param>
        /// <returns>System.String.</returns>
        public static string Next(this IterationInfo info) => info.Args[info.Index + 1];

        /// <summary>
        ///     Returns the args from the read head to the end
        /// </summary>
        /// <param name="info">The information.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        public static IEnumerable<string> Rest(this IterationInfo info) => info.Args.Skip(info.Index + 1);
    }
}