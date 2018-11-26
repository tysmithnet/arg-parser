// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 11-07-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="ContextExtensions.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    /// <summary>
    ///     Convenience extension methods for IContext instances
    /// </summary>
    public static class ContextExtensions
    {
        /// <summary>
        ///     Returns the parsers from the provided to the root, both ends inclusive
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="parserId">The parser identifier.</param>
        /// <returns> the parsers from the provided to the root, both ends inclusive</returns>
        public static IEnumerable<Parser> PathToRoot(this IContext context, string parserId)
        {
            var ids = parserId.ToEnumerableOfOne().Concat(context.HierarchyRepository.GetAncestors(parserId));
            return ids.Select(i => context.ParserRepository.Get(i));
        }

        /// <summary>
        ///     Finds the rot parser for a context
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The root parser</returns>
        public static Parser RootParser(this IContext context)
        {
            var rootId = context.ThrowIfArgumentNull(nameof(context)).HierarchyRepository.GetRoot();
            return context.ParserRepository.Get(rootId);
        }
    }
}