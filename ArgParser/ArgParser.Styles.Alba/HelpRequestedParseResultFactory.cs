// ***********************************************************************
// Assembly         : ArgParser.Styles.Alba
// Author           : @tysmithnet
// Created          : 11-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-19-2018
// ***********************************************************************
// <copyright file="HelpRequestedParseResultFactory.cs" company="ArgParser.Styles.Alba">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    /// <summary>
    ///     ParseResultFactory that decorates an existing factory with hooks to identify when
    ///     help is requested and then display the appropriate help screen
    /// </summary>
    /// <seealso cref="ArgParser.Styles.IParseResultFactory" />
    public class HelpRequestedParseResultFactory : IParseResultFactory
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="HelpRequestedParseResultFactory" /> class.
        /// </summary>
        /// <param name="inner">The inner.</param>
        /// <param name="helpRequestedCallback">The help requested callback.</param>
        /// <param name="context">The context.</param>
        public HelpRequestedParseResultFactory(IParseResultFactory inner,
            Func<Dictionary<object, Parser>, IEnumerable<ParseException>, string> helpRequestedCallback,
            IContext context)
        {
            Inner = inner.ThrowIfArgumentNull(nameof(inner));
            IsHelpRequestedCallback = helpRequestedCallback.ThrowIfArgumentNull(nameof(helpRequestedCallback));
            Context = context.ThrowIfArgumentNull(nameof(context));
        }

        /// <summary>
        ///     Creates the specified results.
        /// </summary>
        /// <param name="results">The results.</param>
        /// <param name="parseExceptions">The parse exceptions.</param>
        /// <returns>IParseResult.</returns>
        public IParseResult Create(Dictionary<object, Parser> results, IEnumerable<ParseException> parseExceptions)
        {
            parseExceptions = parseExceptions.PreventNull().ToList();
            var helpRequest = IsHelpRequestedCallback(results, parseExceptions);
            if (!helpRequest.IsNotNullOrWhiteSpace()) return Inner.Create(results, parseExceptions);
            var template = new ParserHelpTemplate(Context, helpRequest);
            TemplateRenderer.Render(template);
            return new ParseResult(null, null);
        }

        /// <summary>
        ///     Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        public IContext Context { get; set; }

        /// <summary>
        ///     Gets or sets the inner.
        /// </summary>
        /// <value>The inner.</value>
        public IParseResultFactory Inner { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the template renderer.
        /// </summary>
        /// <value>The template renderer.</value>
        public ITemplateRenderer TemplateRenderer { get; protected internal set; } = new TemplateRenderer();

        /// <summary>
        ///     Gets or sets the is help requested callback.
        /// </summary>
        /// <value>The is help requested callback.</value>
        protected internal Func<Dictionary<object, Parser>, IEnumerable<ParseException>, string> IsHelpRequestedCallback
        {
            get;
            set;
        }
    }
}