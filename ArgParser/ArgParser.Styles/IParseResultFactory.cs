// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="IParseResultFactory.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    ///     Represents something that is capable of creating IParseResult
    /// </summary>
    /// <seealso cref="ArgParser.Styles.IParseStrategyUnit" />
    public interface IParseResultFactory : IParseStrategyUnit
    {
        /// <summary>
        ///     Creates the specified results.
        /// </summary>
        /// <param name="results">The results.</param>
        /// <param name="parseExceptions">The parse exceptions.</param>
        /// <returns>IParseResult.</returns>
        IParseResult Create(Dictionary<object, Parser> results, IEnumerable<ParseException> parseExceptions);
    }
}