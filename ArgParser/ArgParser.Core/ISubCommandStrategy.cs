// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-17-2018
// ***********************************************************************
// <copyright file="ISubCommandStrategy.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace ArgParser.Core
{
    /// <summary>
    ///     Represents a parsing strategy that is capable of identifying and parsing sub commands
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ArgParser.Core.IParsingStrategy{T}" />
    public interface ISubCommandStrategy<T> : IParsingStrategy<T>
    {
        /// <summary>
        ///     Determines if the current token is a sub command
        /// </summary>
        /// <param name="subCommands">The sub commands to consider.</param>
        /// <param name="info">The iteration information.</param>
        /// <returns><c>true</c> if the current token is a sub command otherwise, <c>false</c>.</returns>
        bool IsSubCommand(IList<ISubCommand> subCommands, IIterationInfo info);

        /// <summary>
        ///     Parse the arguments using the provided sub commands
        /// </summary>
        /// <param name="subCommands">The sub commands to consider.</param>
        /// <param name="info">The iteration information</param>
        /// <returns>ParseResult.</returns>
        ParseResult Parse(IList<ISubCommand> subCommands, IIterationInfo info);
    }
}