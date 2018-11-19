// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="ParseResult.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    ///     Default IParseResult
    /// </summary>
    /// <seealso cref="ArgParser.Core.IParseResult" />
    public class ParseResult : IParseResult
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ParseResult" /> class.
        /// </summary>
        /// <param name="results">The results.</param>
        /// <param name="parseExceptions">The parse exceptions.</param>
        public ParseResult(Dictionary<object, Parser> results, IEnumerable<ParseException> parseExceptions)
        {
            Results = results ?? new Dictionary<object, Parser>();
            ParseExceptions = parseExceptions.PreventNull().ToList();
        }

        /// <summary>
        ///     Executes a handler on all instances of a certain type
        /// </summary>
        /// <typeparam name="T">The type of a parsed instance of interest</typeparam>
        /// <param name="handler">The handler.</param>
        public void When<T>(Action<T, Parser> handler)
        {
            foreach (var kvp in Results)
                if (kvp.Key is T casted)
                    handler(casted, kvp.Value);
        }

        /// <summary>
        ///     Executes a handler on all parse exceptions that occurred during processing
        /// </summary>
        /// <param name="handler">The handler.</param>
        public void WhenError(Action<IEnumerable<ParseException>> handler)
        {
            if (ParseExceptions.Any())
                handler(ParseExceptions);
        }

        /// <summary>
        ///     Gets or sets the parse exceptions.
        /// </summary>
        /// <value>The parse exceptions.</value>
        protected internal IList<ParseException> ParseExceptions { get; set; }

        /// <summary>
        ///     Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        protected internal Dictionary<object, Parser> Results { get; set; }
    }
}