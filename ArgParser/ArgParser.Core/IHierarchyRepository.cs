// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 11-04-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-10-2018
// ***********************************************************************
// <copyright file="IHierarchyRepository.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace ArgParser.Core
{
    /// <summary>
    /// Represents an object that is capable of managing the relationships between
    /// parsers
    /// </summary>
    public interface IHierarchyRepository
    {
        /// <summary>
        /// Establishes a parent child relationship between parsers.
        /// </summary>
        /// <param name="parentParserId">The parent parser identifier.</param>
        /// <param name="childParserId">The child parser identifier.</param>
        void EstablishParentChildRelationship(string parentParserId, string childParserId);
        /// <summary>
        /// Gets the ancestors of a parser
        /// </summary>
        /// <param name="parserId">The parser identifier.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        IEnumerable<string> GetAncestors(string parserId);
        /// <summary>
        /// Gets the children id's of a parser
        /// </summary>
        /// <param name="parserId">The parser identifier.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        IEnumerable<string> GetChildren(string parserId);
        /// <summary>
        /// Gets the root parser
        /// </summary>
        /// <returns>The root parser id</returns>
        string GetRoot();
        /// <summary>
        /// Determines whether the specified parent parser identifier is parent.
        /// </summary>
        /// <param name="parentParserId">The parent parser identifier.</param>
        /// <param name="childParserId">The child parser identifier.</param>
        /// <returns><c>true</c> if the specified parent parser identifier is parent; otherwise, <c>false</c>.</returns>
        bool IsParent(string parentParserId, string childParserId);
    }
}