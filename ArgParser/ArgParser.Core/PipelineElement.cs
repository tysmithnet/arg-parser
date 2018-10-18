// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-16-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-17-2018
// ***********************************************************************
// <copyright file="CommandLineElement.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using ArgParser.Core.Help;

namespace ArgParser.Core
{
    /// <summary>
    ///     Describes functionality that is capable of determining when to stop token consumption
    /// </summary>
    /// <param name="info">The iteration information</param>
    /// <param name="token">The token.</param>
    /// <param name="tokenNumber">The token number.</param>
    /// <returns><c>true</c> if the current token can be consumed, <c>false</c> otherwise.</returns>
    public delegate bool TakeWhileCallback(IIterationInfo info, string token, int tokenNumber);

    /// <summary>
    ///     Represents an object that is part of the argument parsing pipeline
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class PipelineElement<T> : IHelpful
    {
        /// <summary>
        ///     Gets or sets the functionality capable of identifying tokens to be consumed
        /// </summary>
        /// <value>The take while.</value>
        public TakeWhileCallback TakeWhile { get; set; }

        /// <summary>
        ///     Gets or sets the transformer.
        /// </summary>
        /// <value>The transformer.</value>
        public TransformerCallback Transformer { get; set; }

        /// <summary>
        ///     Describes functionality that is capable of using the consumed strings to modify the options currently being
        ///     constructed
        /// </summary>
        /// <param name="info">The iteration information</param>
        /// <param name="instance">The options instance currently being constructed.</param>
        /// <param name="consumedStrings">The consumed strings.</param>
        public delegate void TransformerCallback(IIterationInfo info, T instance, string[] consumedStrings);

        /// <inheritdoc />
        public IHelpHints HelpHints { get; protected internal set; }
    }
}