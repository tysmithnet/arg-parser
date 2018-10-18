// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-17-2018
// ***********************************************************************
// <copyright file="ISubCommand.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace ArgParser.Core
{
    /// <summary>
    /// Interface ISubCommand
    /// </summary>
    public interface ISubCommand
    {
        /// <summary>
        /// Gets or sets the is command.
        /// </summary>
        /// <value>The is command.</value>
        Func<IterationInfo, bool> IsCommand { get; set; }
        /// <summary>
        /// Parses the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>ParseResult.</returns>
        ParseResult Parse(string[] args);
    }
}