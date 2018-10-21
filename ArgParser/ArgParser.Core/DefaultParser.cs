// ***********************************************************************
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
    /// <seealso cref="ArgParser.Core.ISwitchContainer{T}" />
    public class DefaultParser<T, TBase> : IParser<T, TBase>, ISwitchContainer<T> where T : TBase
    {
        /// <summary>
        ///     Adds the switch.
        /// </summary>
        /// <param name="svitch">The svitch.</param>
        /// <inheritdoc />
        public void AddSwitch(ISwitch<T> svitch)
        {
            DefaultParserInternal.AddSwitch(svitch);
        }

        /// <summary>
        ///     Determines whether this instance can handle the specified instance.
        /// </summary>
        /// <typeparam name="TSub">The type of the t sub.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="info">The information.</param>
        /// <returns><c>true</c> if this instance can handle the specified instance; otherwise, <c>false</c>.</returns>
        /// <inheritdoc />
        public bool CanHandle<TSub>(TSub instance, IIterationInfo info) where TSub : T =>
            DefaultParserInternal.CanHandle(instance, info) || BaseParser.CanHandle(instance, info);

        /// <summary>
        ///     Handles the specified instance.
        /// </summary>
        /// <typeparam name="TSub">The type of the t sub.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="info">The information.</param>
        /// <returns>IIterationInfo.</returns>
        /// <inheritdoc />
        public IIterationInfo Handle<TSub>(TSub instance, IIterationInfo info) where TSub : T
        {
            if (DefaultParserInternal.CanHandle(instance, info))
                return DefaultParserInternal.Handle(instance, info);
            return BaseParser?.Handle(instance, info) ?? info;
        }

        /// <summary>
        ///     Gets or sets the base parser.
        /// </summary>
        /// <value>The base parser.</value>
        /// <inheritdoc />
        public IParser<TBase> BaseParser { get; set; }

        /// <summary>
        ///     Gets the default parser internal.
        /// </summary>
        /// <value>The default parser internal.</value>
        private DefaultParser<T> DefaultParserInternal { get; } = new DefaultParser<T>();
    }

    /// <summary>
    ///     Class DefaultParser.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ArgParser.Core.IParser{T}" />
    /// <seealso cref="ArgParser.Core.ISwitchContainer{T}" />
    public class DefaultParser<T> : IParser<T>, ISwitchContainer<T>
    {
        /// <summary>
        ///     Adds the switch.
        /// </summary>
        /// <param name="svitch">The svitch.</param>
        public void AddSwitch(ISwitch<T> svitch)
        {
            Switches.Add(svitch);
        }

        /// <summary>
        ///     Determines whether this instance can handle the specified instance.
        /// </summary>
        /// <typeparam name="TSub">The type of the t sub.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="info">The information.</param>
        /// <returns><c>true</c> if this instance can handle the specified instance; otherwise, <c>false</c>.</returns>
        /// <inheritdoc />
        public virtual bool CanHandle<TSub>(TSub instance, IIterationInfo info) where TSub : T
        {
            if (Switches.Any(s => s.CanHandle?.Invoke(instance, info) ?? false)) return true;

            return false;
        }

        /// <summary>
        ///     Handles the specified instance.
        /// </summary>
        /// <typeparam name="TSub">The type of the t sub.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="info">The information.</param>
        /// <returns>IIterationInfo.</returns>
        /// <inheritdoc />
        public virtual IIterationInfo Handle<TSub>(TSub instance, IIterationInfo info) where TSub : T
        {
            var svitch = Switches.FirstOrDefault(s => s.CanHandle?.Invoke(instance, info) ?? false);
            return svitch?.Handle?.Invoke(instance, info) ?? info;
        }

        /// <summary>
        ///     Gets the switches.
        /// </summary>
        /// <value>The switches.</value>
        protected internal IList<ISwitch<T>> Switches { get; } = new List<ISwitch<T>>();
    }
}