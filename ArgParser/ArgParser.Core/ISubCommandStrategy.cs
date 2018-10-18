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
    /// Interface ISubCommandStrategy
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ArgParser.Core.IParsingStrategy{T}" />
    public interface ISubCommandStrategy<T> : IParsingStrategy<T>
    {
        /// <summary>
        /// Determines whether [is sub command] [the specified sub commands].
        /// </summary>
        /// <param name="subCommands">The sub commands.</param>
        /// <param name="info">The information.</param>
        /// <returns><c>true</c> if [is sub command] [the specified sub commands]; otherwise, <c>false</c>.</returns>
        bool IsSubCommand(IList<ISubCommand> subCommands, IterationInfo info);
        /// <summary>
        /// Parses the specified sub commands.
        /// </summary>
        /// <param name="subCommands">The sub commands.</param>
        /// <param name="info">The information.</param>
        /// <returns>ParseResult.</returns>
        ParseResult Parse(IList<ISubCommand> subCommands, IterationInfo info);
    }
}