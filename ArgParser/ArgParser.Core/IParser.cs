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

using ArgParser.Core.Help;

namespace ArgParser.Core
{
    public interface IParser : IHelpful
    {
        bool CanConsume(object instance, IIterationInfo info);
        IIterationInfo Consume(object instance, IIterationInfo info);
        IParser BaseParser { get; }
    }

    /// <summary>
    ///     Interface IParser
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IParser<in T> : IParser
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
}