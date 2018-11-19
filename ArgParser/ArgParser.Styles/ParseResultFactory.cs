// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="ParseResultFactory.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    /// Default IParseResultFactory implementation
    /// </summary>
    /// <seealso cref="ArgParser.Styles.IParseResultFactory" />
    public class ParseResultFactory : IParseResultFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParseResultFactory"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ParseResultFactory(IContext context)
        {
            Context = context.ThrowIfArgumentNull(nameof(context));
        }

        /// <summary>
        /// Creates the specified results.
        /// </summary>
        /// <param name="results">The results.</param>
        /// <param name="parseExceptions">The parse exceptions.</param>
        /// <returns>IParseResult.</returns>
        public IParseResult Create(Dictionary<object, Parser> results, IEnumerable<ParseException> parseExceptions) =>
            new ParseResult(results, parseExceptions);

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        public IContext Context { get; set; }
    }
}