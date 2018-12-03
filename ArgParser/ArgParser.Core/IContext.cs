// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 11-04-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="IContext.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ArgParser.Core
{
    /// <summary>
    ///     Represents the environment in which parsing occurs. It is a collection
    ///     of high level interfaces that provide access to core features.
    /// </summary>
    public interface IContext
    {
        /// <summary>
        ///     Gets the alias repository.
        /// </summary>
        /// <value>The alias repository.</value>
        IAliasRepository AliasRepository { get; }

        /// <summary>
        ///     Gets the hierarchy repository.
        /// </summary>
        /// <value>The hierarchy repository.</value>
        IHierarchyRepository HierarchyRepository { get; }

        /// <summary>
        ///     Gets the parser repository.
        /// </summary>
        /// <value>The parser repository.</value>
        IParserRepository ParserRepository { get; }
    }
}