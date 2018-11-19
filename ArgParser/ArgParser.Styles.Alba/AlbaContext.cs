// ***********************************************************************
// Assembly         : ArgParser.Styles.Alba
// Author           : @tysmithnet
// Created          : 11-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="AlbaContext.cs" company="ArgParser.Styles.Alba">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    /// <summary>
    ///     IContext decorator for Alba integrations
    /// </summary>
    /// <seealso cref="ArgParser.Core.IContext" />
    public class AlbaContext : IContext
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AlbaContext" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public AlbaContext(IContext context)
        {
            Context = context.ThrowIfArgumentNull(nameof(context));
        }

        /// <summary>
        ///     Gets the alias repository.
        /// </summary>
        /// <value>The alias repository.</value>
        public IAliasRepository AliasRepository => Context.AliasRepository;

        /// <summary>
        ///     Gets the hierarchy repository.
        /// </summary>
        /// <value>The hierarchy repository.</value>
        public IHierarchyRepository HierarchyRepository => Context.HierarchyRepository;

        /// <summary>
        ///     Gets the parser repository.
        /// </summary>
        /// <value>The parser repository.</value>
        public IParserRepository ParserRepository => Context.ParserRepository;

        /// <summary>
        ///     Gets or sets the theme repository.
        /// </summary>
        /// <value>The theme repository.</value>
        public IThemeRepository ThemeRepository { get; protected internal set; } = new ThemeRepository();

        /// <summary>
        ///     Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        protected internal IContext Context { get; set; }
    }
}