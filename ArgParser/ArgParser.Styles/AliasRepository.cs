// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="AliasRepository.cs" company="ArgParser.Styles">
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
    ///     Something that is capable of managing the aliases of parsers
    /// </summary>
    /// <seealso cref="ArgParser.Core.IAliasRepository" />
    public class AliasRepository : IAliasRepository
    {
        /// <summary>
        ///     Gets the alias for the provided parser.
        /// </summary>
        /// <param name="parserId">The parser identifier.</param>
        /// <returns>The alias for the provided parser</returns>
        public virtual string GetAlias(string parserId) => Aliases[parserId];

        /// <summary>
        ///     Determines whether the specified parser identifier has an alias.
        /// </summary>
        /// <param name="parserId">The parser identifier.</param>
        /// <returns><c>true</c> if the specified parser identifier has an alias; otherwise, <c>false</c>.</returns>
        public virtual bool HasAlias(string parserId) => Aliases.ContainsKey(parserId);

        /// <summary>
        ///     Finds any parser ids that the provided alias exists for
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>Any parser ids that the provided alias exists for</returns>
        public virtual IEnumerable<string> Lookup(string alias)
        {
            return Aliases.Where(kvp => kvp.Value == alias).Select(x => x.Key);
        }

        /// <summary>
        ///     Sets the alias for the specified parser
        /// </summary>
        /// <param name="parserId">The parser identifier.</param>
        /// <param name="alias">The alias.</param>
        public virtual void SetAlias(string parserId, string alias)
        {
            Aliases[parserId] = alias;
        }

        /// <summary>
        ///     Gets or sets the aliases.
        /// </summary>
        /// <value>The aliases.</value>
        protected internal Dictionary<string, string> Aliases { get; set; } = new Dictionary<string, string>();
    }
}