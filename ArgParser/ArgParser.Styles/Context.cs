// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-12-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="Context.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    ///     Default context implementation
    /// </summary>
    /// <seealso cref="ArgParser.Core.IContext" />
    public class Context : IContext
    {
        /// <summary>
        ///     Gets the alias repository.
        /// </summary>
        /// <value>The alias repository.</value>
        public IAliasRepository AliasRepository { get; protected internal set; } = new AliasRepository();

        /// <summary>
        ///     Gets the hierarchy repository.
        /// </summary>
        /// <value>The hierarchy repository.</value>
        public IHierarchyRepository HierarchyRepository { get; protected internal set; } = new HierarchyRepository();

        /// <summary>
        ///     Gets the parser repository.
        /// </summary>
        /// <value>The parser repository.</value>
        public IParserRepository ParserRepository { get; protected internal set; } = new ParserRepository();
    }
}