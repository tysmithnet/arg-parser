// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-17-2018
// ***********************************************************************
// <copyright file="ISwitchStrategy.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace ArgParser.Core
{
    /// <summary>
    /// Represents an object capable of identifying if a token is currently being iterated over
    /// and of consuming additional tokens to statisfy the switch.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ArgParser.Core.IParsingStrategy{T}" />
    public interface ISwitchStrategy<T> : IParsingStrategy<T>
    {
        /// <summary>
        /// Consume tokens to statisfy the provided switches if they require it
        /// </summary>
        /// <param name="switches">The switches to consider.</param>
        /// <param name="instance">The options object currently being constructed.</param>
        /// <param name="info">The information about the current iteration over the arguments.</param>
        /// <returns>IterationInfo.</returns>
        IterationInfo ConsumeSwitch(IList<Switch<T>> switches, T instance, IterationInfo info);
        /// <summary>
        /// Determines if the current token is a switch
        /// </summary>
        /// <param name="switches">The switches to consider.</param>
        /// <param name="info">The information.</param>
        /// <returns><c>true</c> if the current token is a switch; otherwise, <c>false</c>.</returns>
        bool IsSwitch(IList<Switch<T>> switches, IterationInfo info);
        /// <summary>
        /// Determines if the current token is a group of switches e.g. grep -rnw
        /// </summary>
        /// <param name="switches">The switches to consider.</param>
        /// <param name="info">The information about the current iteration over the arguments.</param>
        /// <returns><c>true</c> if the current token is a switch group; otherwise, <c>false</c>.</returns>
        bool IsGroup(IList<Switch<T>> switches, IterationInfo info);
        /// <summary>
        /// Consumes the group.
        /// </summary>
        /// <param name="switches">The switches.</param>
        /// <param name="instance">The instance.</param>
        /// <param name="info">The information about the current iteration over the arguments.</param>
        /// <returns>IterationInfo.</returns>
        IterationInfo ConsumeGroup(IList<Switch<T>> switches, T instance, IterationInfo info);
    }
}