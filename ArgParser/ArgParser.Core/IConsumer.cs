// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 11-04-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="IConsumer.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ArgParser.Core
{
    /// <summary>
    ///     Represents an object that is capable of consuming args and populating an instance
    /// </summary>
    public interface IConsumer
    {
        /// <summary>
        ///     Determines whether this instance can consume the args and populate the provided instance
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="info">The information.</param>
        /// <returns>ConsumptionResult.</returns>
        ConsumptionResult CanConsume(object instance, IterationInfo info);

        /// <summary>
        ///     Consumes the args and populate the provided instance
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="request">The request.</param>
        /// <returns>ConsumptionResult.</returns>
        ConsumptionResult Consume(object instance, ConsumptionRequest request);

        /// <summary>
        ///     Resets this instance.
        /// </summary>
        void Reset();
    }

    /// <summary>
    ///     Represents an object that is capable of consuming args and populating an instance
    /// </summary>
    /// <typeparam name="T">The type of instances this is capable of populating</typeparam>
    /// <seealso cref="ArgParser.Core.IConsumer" />
    public interface IConsumer<in T> : IConsumer
    {
        /// <summary>
        ///     Determines whether this instance can consume the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="info">The information.</param>
        /// <returns>ConsumptionResult.</returns>
        ConsumptionResult CanConsume(T instance, IterationInfo info);

        /// <summary>
        ///     Consumes the args and populate the provided instance
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="request">The request.</param>
        /// <returns>ConsumptionResult.</returns>
        ConsumptionResult Consume(T instance, ConsumptionRequest request);
    }
}