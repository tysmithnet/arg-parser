// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 11-04-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="Parameter.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;

namespace ArgParser.Core
{
    /// <summary>
    /// Represents a component of a parser that is able to contribute the eventual
    /// configuration of an option.
    /// </summary>
    /// <seealso cref="ArgParser.Core.IConsumer" />
    public abstract class Parameter : IConsumer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Parameter"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="consumeCallback">The consume callback.</param>
        protected Parameter(Parser parent, Action<object, string[]> consumeCallback)
        {
            Parser = parent.ThrowIfArgumentNull(nameof(parent));
            ConsumeCallback = consumeCallback.ThrowIfArgumentNull(nameof(consumeCallback));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Parameter"/> class.
        /// </summary>
        protected Parameter()
        {
        }

        /// <summary>
        /// Determines whether this parameter can consume the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="info">The information.</param>
        /// <returns>The result of the consumption</returns>
        public abstract ConsumptionResult CanConsume(object instance, IterationInfo info);

        /// <summary>
        /// Consumes the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="request">The request.</param>
        /// <returns>ConsumptionResult.</returns>
        /// <exception cref="MissingValueException"></exception>
        public virtual ConsumptionResult Consume(object instance, ConsumptionRequest request)
        {
            try
            {
                if (request.Max < MinRequired)
                    throw new MissingValueException(this,
                        $"Switch {this} expected to have at least {MinRequired} values, but was told it can only have {request.Max}. Are you sure you passed enough values to satisfy the switch?");
                HasBeenConsumed = true;
                var values = request.AllToBeConsumed().Take(MaxAllowed).ToArray();
                ConsumeCallback(instance, values);
                return new ConsumptionResult(request.Info, values.Length, this);
            }
            catch (ParseException pe)
            {
                return new ConsumptionResult(pe);
            }
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void Reset()
        {
            HasBeenConsumed = false;
        }

        /// <summary>
        /// Gets or sets the consume callback.
        /// </summary>
        /// <value>The consume callback.</value>
        public Action<object, string[]> ConsumeCallback { get; protected internal set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has been consumed.
        /// </summary>
        /// <value><c>true</c> if this instance has been consumed; otherwise, <c>false</c>.</value>
        public bool HasBeenConsumed { get; protected internal set; }

        /// <summary>
        /// Gets or sets the help.
        /// </summary>
        /// <value>The help.</value>
        public ParameterHelp Help { get; set; } = new ParameterHelp();
        /// <summary>
        /// Gets or sets the maximum allowed.
        /// </summary>
        /// <value>The maximum allowed.</value>
        public int MaxAllowed { get; set; } = int.MaxValue;
        /// <summary>
        /// Gets or sets the minimum required.
        /// </summary>
        /// <value>The minimum required.</value>
        public int MinRequired { get; set; } = 1;
        /// <summary>
        /// Gets or sets the parser.
        /// </summary>
        /// <value>The parser.</value>
        public Parser Parser { get; protected internal set; }
    }
}