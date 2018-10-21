// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-20-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-20-2018
// ***********************************************************************
// <copyright file="IParser.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ArgParser.Core
{
    /// <summary>
    ///     Interface IParser
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IParser<in T>
    {
        /// <summary>
        ///     Determines whether this instance can handle the specified instance.
        /// </summary>
        /// <typeparam name="TSub">The type of the t sub.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="info">The information.</param>
        /// <returns><c>true</c> if this instance can handle the specified instance; otherwise, <c>false</c>.</returns>
        bool CanConsume<TSub>(TSub instance, IIterationInfo info) where TSub : T;

        /// <summary>
        ///     Handles the specified instance.
        /// </summary>
        /// <typeparam name="TSub">The type of the t sub.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="info">The information.</param>
        /// <returns>IIterationInfo.</returns>
        IIterationInfo Consume<TSub>(TSub instance, IIterationInfo info) where TSub : T;
    }

    /// <summary>
    ///     Interface IParser
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TBase">The type of the t base.</typeparam>
    /// <seealso cref="ArgParser.Core.IParser{T}" />
    public interface IParser<in T, in TBase> : IParser<T> where T : TBase
    {
        /// <summary>
        ///     Gets the base parser.
        /// </summary>
        /// <value>The base parser.</value>
        IParser<TBase> BaseParser { get; }
    }
}