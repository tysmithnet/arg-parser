// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-17-2018
// ***********************************************************************
// <copyright file="IPositionalStrategy.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace ArgParser.Core
{
    /// <summary>
    /// Interface IPositionalStrategy
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ArgParser.Core.IParsingStrategy{T}" />
    public interface IPositionalStrategy<T> : IParsingStrategy<T>
    {
        /// <summary>
        /// Consumes the specified positionals.
        /// </summary>
        /// <param name="positionals">The positionals.</param>
        /// <param name="instance">The instance.</param>
        /// <param name="info">The information.</param>
        /// <returns>IterationInfo.</returns>
        IterationInfo Consume(IList<Positional<T>> positionals, T instance, IterationInfo info);
        /// <summary>
        /// Determines whether the specified positionals is positional.
        /// </summary>
        /// <param name="positionals">The positionals.</param>
        /// <param name="info">The information.</param>
        /// <returns><c>true</c> if the specified positionals is positional; otherwise, <c>false</c>.</returns>
        bool IsPositional(IList<Positional<T>> positionals, IterationInfo info);
    }
}