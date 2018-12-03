// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 11-04-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-25-2018
// ***********************************************************************
// <copyright file="IParserRepository.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace ArgParser.Core
{
    /// <summary>
    ///     Represents an object that is capable of managing the collection of parsers available
    /// </summary>
    public interface IParserRepository
    {
        /// <summary>
        ///     Determines whether there is a parser registered with a specific id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns><c>true</c> if there is a parser registered with a specific id otherwise, <c>false</c>.</returns>
        bool Contains(string id);

        /// <summary>
        ///     Creates a parser with the specified id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The created parser</returns>
        Parser Create(string id);

        /// <summary>
        ///     Creates a parser with the specified id
        /// </summary>
        /// <typeparam name="T">The type of the instance the parser can create</typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns>The created parser</returns>
        Parser<T> Create<T>(string id);

        /// <summary>
        ///     Gets the parser with the specified id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Existing Parser.</returns>
        Parser Get(string id);

        /// <summary>
        ///     Gets the parser with the specified id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns>Existing parser</returns>
        Parser<T> Get<T>(string id);

        /// <summary>
        ///     Gets all parser
        /// </summary>
        /// <returns>All parser</returns>
        IEnumerable<Parser> GetAll();
    }
}