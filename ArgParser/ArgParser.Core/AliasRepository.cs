// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="AliasRepository.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace ArgParser.Core
{
    /// <summary>
    ///     Represents an object that is capable of orchestrating the management of parser
    ///     aliases
    /// </summary>
    public interface IAliasRepository
    {
        /// <summary>
        ///     Gets the alias for the provided parser.
        /// </summary>
        /// <param name="parserId">The parser identifier.</param>
        /// <returns>The alias for the provided parser</returns>
        string GetAlias(string parserId);

        /// <summary>
        ///     Determines whether the specified parser identifier has an alias.
        /// </summary>
        /// <param name="parserId">The parser identifier.</param>
        /// <returns><c>true</c> if the specified parser identifier has an alias; otherwise, <c>false</c>.</returns>
        bool HasAlias(string parserId);

        /// <summary>
        ///     Finds any parser ids that the provided alias exists for
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>Any parser ids that the provided alias exists for</returns>
        IEnumerable<string> Lookup(string alias);

        /// <summary>
        ///     Sets the alias for the specified parser
        /// </summary>
        /// <param name="parserId">The parser identifier.</param>
        /// <param name="alias">The alias.</param>
        void SetAlias(string parserId, string alias);
    }
}