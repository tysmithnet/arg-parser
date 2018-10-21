﻿// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-20-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-21-2018
// ***********************************************************************
// <copyright file="DefaultParser.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    /// <summary>
    ///     Class DefaultParser.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TBase">The type of the t base.</typeparam>
    /// <seealso cref="ArgParser.Core.IParser{T, TBase}" />
    /// <seealso cref="IParameterContainer{T}" />
    public class DefaultParser<T> : IParser<T>, IParameterContainer<T>
    {
        public IList<IParameter<T>> Parameters { get; set; } = new List<IParameter<T>>();

        /// <summary>
        ///     Adds the switch.
        /// </summary>
        /// <param name="parameter">The svitch.</param>
        /// <inheritdoc />
        public void AddParameter(IParameter<T> parameter)
        {
            Parameters.Add(parameter);
        }

        /// <summary>
        ///     Determines whether this instance can handle the specified instance.
        /// </summary>
        /// <typeparam name="TSub">The type of the t sub.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="info">The information.</param>
        /// <returns><c>true</c> if this instance can handle the specified instance; otherwise, <c>false</c>.</returns>
        /// <inheritdoc />
        public bool CanConsume<TSub>(TSub instance, IIterationInfo info) where TSub : T
        {
            return Parameters.Any(p => p.CanHandle(instance, info)) ||
                   (BaseParser?.CanConsume(instance, info) ?? false);
        }

        /// <summary>
        ///     Handles the specified instance.
        /// </summary>
        /// <typeparam name="TSub">The type of the t sub.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="info">The information.</param>
        /// <returns>IIterationInfo.</returns>
        /// <inheritdoc />
        public IIterationInfo Consume<TSub>(TSub instance, IIterationInfo info) where TSub : T
        {
            var first = Parameters.FirstOrDefault(p => p.CanHandle(instance, info));
            var result = first?.Handle(instance, info) ?? BaseParser?.Consume(instance, info);
            if(result == null)
                throw new InvalidOperationException($"CanConsume determined that instance: {instance}, could be consumed, but failed during consumption");
            return result;
        }

        /// <summary>
        ///     Gets or sets the base parser.
        /// </summary>
        /// <value>The base parser.</value>
        /// <inheritdoc />
        public IParser BaseParser { get; set; }

        /// <summary>
        ///     Gets the default parser internal.
        /// </summary>
        /// <value>The default parser internal.</value>

        /// <inheritdoc />
        public bool CanConsume(object instance, IIterationInfo info)
        {
            if (instance is T casted)
                return CanConsume(casted, info);
            return BaseParser?.CanConsume(instance, info) ?? false;
        }

        /// <inheritdoc />
        public IIterationInfo Consume(object instance, IIterationInfo info)
        {
            if (instance is T casted)
            {
                return Consume(casted, info);
            }

            return BaseParser?.Consume(instance, info);
        }
    }
}