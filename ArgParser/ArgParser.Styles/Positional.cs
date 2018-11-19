// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-12-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-12-2018
// ***********************************************************************
// <copyright file="Positional.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    /// Represents a parameter who's significance is derived from the order in which it appears
    /// </summary>
    /// <seealso cref="ArgParser.Core.Parameter" />
    /// <seealso cref="ArgParser.Styles.IRequirable" />
    public class Positional : Parameter, IRequirable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Positional"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="consumeCallback">The consume callback.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        public Positional(Parser parent, Action<object, string[]> consumeCallback, int min = 1,
            int max = int.MaxValue) : base(parent,
            consumeCallback)
        {
            MinRequired = min;
            MaxAllowed = max;
        }

        /// <summary>
        /// Determines whether this parameter can consume the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="info">The information.</param>
        /// <returns>The result of the consumption</returns>
        public override ConsumptionResult CanConsume(object instance, IterationInfo info)
        {
            if (HasBeenConsumed)
                return new ConsumptionResult(info, 0, null);
            var values = info.FromNowOn().Take(MaxAllowed).ToArray();
            return new ConsumptionResult(info, values.Length, this);
        }

        /// <summary>
        /// Gets a value indicating whether this instance is required.
        /// </summary>
        /// <value><c>true</c> if this instance is required; otherwise, <c>false</c>.</value>
        public bool IsRequired { get; protected internal set; }
    }

    /// <summary>
    /// Represents a parameter who's significance is derived from the order in which it appears
    /// </summary>
    /// <typeparam name="T">The type of the instance it can populate</typeparam>
    /// <seealso cref="ArgParser.Styles.Positional" />
    public class Positional<T> : Positional
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Positional{T}"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="consumeCallback">The consume callback.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        public Positional(Parser parent, Action<T, string[]> consumeCallback, int min = 1, int max = int.MaxValue) :
            base(parent,
                consumeCallback.ToNonGenericAction(true), min, max)
        {
        }
    }
}