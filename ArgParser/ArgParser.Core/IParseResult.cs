// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 11-04-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-17-2018
// ***********************************************************************
// <copyright file="IParseResult.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;

namespace ArgParser.Core
{
    /// <summary>
    ///     Represents the result of a parsing attempt
    /// </summary>
    public interface IParseResult
    {
        /// <summary>
        ///     Executes a handler on all instances of a certain type
        /// </summary>
        /// <typeparam name="T">The type of a parsed instance of interest</typeparam>
        /// <param name="handler">The handler.</param>
        void When<T>(Action<T, Parser> handler);

        /// <summary>
        ///     Executes a handler on all parse exceptions that occurred during processing
        /// </summary>
        /// <param name="handler">The handler.</param>
        void WhenError(Action<IEnumerable<ParseException>> handler);
    }
}