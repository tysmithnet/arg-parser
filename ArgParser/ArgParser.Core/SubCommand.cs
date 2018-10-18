// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-17-2018
// ***********************************************************************
// <copyright file="SubCommand.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using ArgParser.Core.Help;

namespace ArgParser.Core
{
    /// <summary>
    ///     A subcommand typically takes has its own set of options. Called verb in other frameworks.
    /// </summary>
    /// <typeparam name="T">Type of the sub command's options</typeparam>
    /// <typeparam name="TParent">The type of the parent parser to which this will be added</typeparam>
    /// <seealso cref="ArgParser.Core.ISubCommand" />
    public class SubCommand<T, TParent> : ISubCommand where T : TParent
    {
        /// <inheritdoc />
        public IdentityInformation Identity { get; set; }

        /// <summary>
        ///     Parses the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>ParseResult.</returns>
        public ParseResult Parse(string[] args) => ArgParser.Parse(args);

        /// <summary>
        ///     Gets or sets the function that determines if the current token should be considered a command
        /// </summary>
        /// <value>The is command.</value>
        /// <inheritdoc />
        public Func<IIterationInfo, bool> IsCommand { get; set; }

        /// <summary>
        ///     Gets or sets the argument parser.
        /// </summary>
        /// <value>The argument parser.</value>
        internal SubCommandArgParser<T, TParent> ArgParser { get; set; }

        /// <inheritdoc />
        public IHelpHints HelpHints { get; protected internal set; }
    }
}