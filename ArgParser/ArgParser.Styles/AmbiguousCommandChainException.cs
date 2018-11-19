// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="AmbiguousCommandChainException.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    ///     An exception that occurs when a sequence of args can be
    /// </summary>
    /// <seealso cref="ArgParser.Core.ParseException" />
    public class AmbiguousCommandChainException : ParseException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AmbiguousCommandChainException" /> class.
        /// </summary>
        /// <param name="matchingLists">The matching lists.</param>
        public AmbiguousCommandChainException(List<List<string>> matchingLists) : base(CreateMessage(matchingLists))
        {
            MatchingSequences = matchingLists.ThrowIfArgumentNull(nameof(matchingLists)).Select(x => x.ToList())
                .ToList();
        }

        /// <summary>
        ///     Creates the message.
        /// </summary>
        /// <param name="matchingLists">The matching lists.</param>
        /// <returns>The exception message</returns>
        private static string CreateMessage(IEnumerable<IEnumerable<string>> matchingLists)
        {
            var lists = matchingLists.Select(x => $"[{x.Join(" ")}]").Join(", ");
            return $"Ambiguous parse chains found: {lists}";
        }

        /// <summary>
        ///     Gets or sets the matching sequences.
        /// </summary>
        /// <value>The matching sequences.</value>
        public IReadOnlyList<IReadOnlyList<string>> MatchingSequences { get; protected internal set; }
    }
}