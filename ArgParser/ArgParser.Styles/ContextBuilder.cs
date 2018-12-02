// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-12-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 12-02-2018
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
        public ContextBuilder()
        {
            Context = new Context
            {
                HierarchyRepository = HierarchyRepository,
                ParserRepository = ParserRepository,
                AliasRepository = AliasRepository
            };
        }

        /// <summary>
        ///     Occurs when a parameter is created
        /// </summary>
        public event EventHandler<ParameterCreatedEventArgs> ParameterCreated;

        /// <summary>
        ///     Occurs when a parser is created.
        /// </summary>
        public event EventHandler<ParserCreatedEventArgs> ParserCreated;

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
            parser = OnParserCreated(parser);
            if (helpSetupCallback == null)
            {
                var newBuilder = new ParserBuilder(this, parser);
                newBuilder.ParameterCreated += (sender, args) =>
                {
                    OnParameterCreated(args); 
                };
                return newBuilder;
            }

            var builder = new ParserHelpBuilder(parser);
            helpSetupCallback(builder);
            parser.Help = builder.Build();
            var parserBuilder = new ParserBuilder(this, parser);
            parserBuilder.ParameterCreated += (sender, args) =>
            {
                OnParameterCreated(args); 
            };
            return parserBuilder;
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
            if (helpSetupCallback == null) return new ParserBuilder<T>(this, parser);
            var builder = new ParserHelpBuilder(parser);
            helpSetupCallback(builder);
            parser.Help = builder.Build();
            var parserBuilder = new ParserBuilder<T>(this, parser);
            parserBuilder.ParameterCreated += (sender, args) => { OnParameterCreated(args); };
            return parserBuilder;
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
            var strat = new ParseStrategy(context);
            strat = OnParseStrategyCreated(strat);
            return strat.Parse(args);
        }

        /// <summary>
        ///     Handles the <see cref="E:ParameterCreated" /> event.
        /// </summary>
        /// <param name="eventArgs">The <see cref="ParameterCreatedEventArgs" /> instance containing the event data.</param>
        protected virtual void OnParameterCreated(ParameterCreatedEventArgs eventArgs)
        {
            ParameterCreated?.Invoke(this, eventArgs);
        }

        /// <summary>
        ///     Called when [parser created].
        /// </summary>
        /// <param name="parser">The parser.</param>
        /// <returns>Parser.</returns>
        protected virtual Parser OnParserCreated(Parser parser)
        {
            var args = new ParserCreatedEventArgs(parser);
            ParserCreated?.Invoke(this, args);
            return args.Parser;
        }

        /// <summary>
        ///     Called when a new parse strategy has been created for use
        /// </summary>
        /// <param name="parseStrategy">The parse strategy.</param>
        /// <returns>ParseStrategy.</returns>
        protected virtual ParseStrategy OnParseStrategyCreated(ParseStrategy parseStrategy)
        {
            var parseStrategyCreatedEventArgs =
                new ParseStrategyCreatedEventArgs(parseStrategy.ThrowIfArgumentNull(nameof(parseStrategy)));
            ParseStrategyCreated?.Invoke(this, parseStrategyCreatedEventArgs);
            return parseStrategyCreatedEventArgs.ParseStrategy;
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
    }
}