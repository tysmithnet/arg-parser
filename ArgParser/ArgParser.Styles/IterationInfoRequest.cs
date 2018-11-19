// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="IterationInfoRequest.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    ///     Represents a request to create iteration info
    /// </summary>
    public class IterationInfoRequest
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="IterationInfoRequest" /> class.
        /// </summary>
        /// <param name="chainIdentificationResult">The chain identification result.</param>
        /// <param name="mutatedArgs">The mutated arguments.</param>
        /// <param name="originalArgs">The original arguments.</param>
        public IterationInfoRequest(ChainIdentificationResult chainIdentificationResult, string[] mutatedArgs,
            string[] originalArgs)
        {
            ChainIdentificationResult =
                chainIdentificationResult.ThrowIfArgumentNull(nameof(chainIdentificationResult));
            MutatedArgs = mutatedArgs.ThrowIfArgumentNull(nameof(mutatedArgs));
            OriginalArgs = originalArgs.ThrowIfArgumentNull(nameof(originalArgs));
        }

        /// <summary>
        ///     Gets or sets the chain identification result.
        /// </summary>
        /// <value>The chain identification result.</value>
        public ChainIdentificationResult ChainIdentificationResult { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the mutated arguments.
        /// </summary>
        /// <value>The mutated arguments.</value>
        public string[] MutatedArgs { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the original arguments.
        /// </summary>
        /// <value>The original arguments.</value>
        public string[] OriginalArgs { get; protected internal set; }
    }
}