// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-19-2018
// ***********************************************************************
// <copyright file="ParseStrategyCreatedEventArgs.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    /// EventArgs for when a new ParseStrategy is created
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ParseStrategyCreatedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the parse strategy.
        /// </summary>
        /// <value>The parse strategy.</value>
        public ParseStrategy ParseStrategy { get; protected internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParseStrategyCreatedEventArgs"/> class.
        /// </summary>
        /// <param name="parseStrategy">The parse strategy.</param>
        public ParseStrategyCreatedEventArgs(ParseStrategy parseStrategy)
        {
            ParseStrategy = parseStrategy.ThrowIfArgumentNull(nameof(parseStrategy));
        }
    }
}