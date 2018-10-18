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
using ArgParser.Core.Help;

namespace ArgParser.Core
{
    /// <summary>
    ///     Represents a subcommand of the application.
    ///     <example>git commit</example>
    /// </summary>
    public interface ISubCommand : IHelpful
    {
        IdentityInformation Identity { get; }

        /// <summary>
        ///     Parses the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>ParseResult.</returns>
        ParseResult Parse(string[] args);

        /// <summary>
        ///     Gets or sets the function that determines if the current token should be considered a command
        /// </summary>
        /// <value>The is command.</value>
        Func<IIterationInfo, bool> IsCommand { get; set; }
    }
}