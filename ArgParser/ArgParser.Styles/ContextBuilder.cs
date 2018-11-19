// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-12-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="ContextBuilder.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    ///     Builder pattern for Context
    /// </summary>
    public class ContextBuilder
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ContextBuilder" /> class.
        /// </summary>
        /// <param name="rootParserId">The root parser identifier.</param>
        public ContextBuilder(string rootParserId)
        {
            RootParserId = rootParserId.ThrowIfArgumentNull(nameof(rootParserId));
            Context = new Context
            {
                HierarchyRepository = HierarchyRepository,
                ParserRepository = ParserRepository,
                AliasRepository = AliasRepository
            };
        }

        /// <summary>
        ///     Occurs when a new parse strategy has been created. Use this if you are interested in controlling
        ///     the finer points of parsing
        /// </summary>
        public event EventHandler<ParseStrategyCreatedEventArgs> ParseStrategyCreated;

        /// <summary>
        ///     Adds a parser.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="helpSetupCallback">The help setup callback.</param>
        /// <returns>ParserBuilder.</returns>
        public ParserBuilder AddParser(string id, Action<ParserHelpBuilder> helpSetupCallback = null)
        {
            var parser = ParserRepository.Create(id);
            HierarchyRepository.AddParser(id);
            if (helpSetupCallback != null)
            {
                var builder = new ParserHelpBuilder(parser);
                helpSetupCallback(builder);
                parser.Help = builder.Build();
            }

            return new ParserBuilder(this, parser);
        }

        /// <summary>
        ///     Adds a parser.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="helpSetupCallback">The help setup callback.</param>
        /// <returns>ParserBuilder&lt;T&gt;.</returns>
        public ParserBuilder<T> AddParser<T>(string id, Action<ParserHelpBuilder> helpSetupCallback = null)
        {
            var parser = ParserRepository.Create<T>(id);
            HierarchyRepository.AddParser(id);

            if (helpSetupCallback != null)
            {
                var builder = new ParserHelpBuilder(parser);
                helpSetupCallback(builder);
                parser.Help = builder.Build();
            }

            return new ParserBuilder<T>(this, parser);
        }

        /// <summary>
        ///     Creates a parent child relationship.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="child">The child.</param>
        /// <returns>ContextBuilder.</returns>
        public ContextBuilder CreateParentChildRelationship(string parent, string child)
        {
            HierarchyRepository.EstablishParentChildRelationship(parent, child);
            return this;
        }

        /// <summary>
        ///     Parses the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>IParseResult.</returns>
        public IParseResult Parse(string[] args)
        {
            var context = Context;
            var strat = new ParseStrategy(context, RootParserId);
            OnParseStrategyCreated(strat);
            return strat.Parse(args);
        }

        /// <summary>
        ///     Called when a new parse strategy has been created for use
        /// </summary>
        /// <param name="parseStrategy">The parse strategy.</param>
        protected virtual void OnParseStrategyCreated(ParseStrategy parseStrategy)
        {
            ParseStrategyCreated?.Invoke(this,
                new ParseStrategyCreatedEventArgs(parseStrategy.ThrowIfArgumentNull(nameof(parseStrategy))));
        }

        /// <summary>
        ///     Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        public Context Context { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the alias repository.
        /// </summary>
        /// <value>The alias repository.</value>
        protected internal AliasRepository AliasRepository { get; set; } = new AliasRepository();

        /// <summary>
        ///     Gets or sets the hierarchy repository.
        /// </summary>
        /// <value>The hierarchy repository.</value>
        protected internal HierarchyRepository HierarchyRepository { get; set; } = new HierarchyRepository();

        /// <summary>
        ///     Gets or sets the parser repository.
        /// </summary>
        /// <value>The parser repository.</value>
        protected internal ParserRepository ParserRepository { get; set; } = new ParserRepository();

        /// <summary>
        ///     Gets or sets the root parser identifier.
        /// </summary>
        /// <value>The root parser identifier.</value>
        protected internal string RootParserId { get; set; }
    }
}