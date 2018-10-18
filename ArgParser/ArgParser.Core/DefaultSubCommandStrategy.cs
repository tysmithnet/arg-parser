// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-17-2018
// ***********************************************************************
// <copyright file="DefaultSubCommandStrategy.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    /// <summary>
    ///     Default sub command strategy implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ArgParser.Core.ISubCommandStrategy{T}" />
    public class DefaultSubCommandStrategy<T> : ISubCommandStrategy<T>
    {
        /// <summary>
        ///     Determines if the current token is a sub command
        /// </summary>
        /// <param name="subCommands">The sub commands to consider.</param>
        /// <param name="info">The iteration information.</param>
        /// <returns><c>true</c> if the current token is a sub command otherwise, <c>false</c>.</returns>
        /// <inheritdoc />
        public bool IsSubCommand(IList<ISubCommand> subCommands, IIterationInfo info)
        {
            return subCommands.Any(s => s.IsCommand(info));
        }

        /// <summary>
        ///     Parse the arguments using the provided sub commands
        /// </summary>
        /// <param name="subCommands">The sub commands to consider.</param>
        /// <param name="info">The iteration information</param>
        /// <returns>ParseResult.</returns>
        /// <inheritdoc />
        public ParseResult Parse(IList<ISubCommand> subCommands, IIterationInfo info)
        {
            var first = subCommands.First(s => s.IsCommand(info));
            return first.Parse(info.AllArgs);
        }

        /// <summary>
        ///     Resets this instance.
        /// </summary>
        /// <inheritdoc />
        public void Reset()
        {
        }
    }
}