// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="PotentialConsumerRequest.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    ///     Represents a request to consume arguments
    /// </summary>
    public class PotentialConsumerRequest
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PotentialConsumerRequest" /> class.
        /// </summary>
        /// <param name="chainIdentificationResult">The chain identification result.</param>
        /// <param name="info">The information.</param>
        /// <param name="instance">The instance.</param>
        public PotentialConsumerRequest(ChainIdentificationResult chainIdentificationResult, IterationInfo info,
            object instance)
        {
            ChainIdentificationResult =
                chainIdentificationResult.ThrowIfArgumentNull(nameof(chainIdentificationResult));
            Info = info.ThrowIfArgumentNull(nameof(info));
            Instance = instance.ThrowIfArgumentNull(nameof(instance));
        }

        /// <summary>
        ///     Gets or sets the chain identification result.
        /// </summary>
        /// <value>The chain identification result.</value>
        public ChainIdentificationResult ChainIdentificationResult { get; set; }

        /// <summary>
        ///     Gets or sets the current iteration info
        /// </summary>
        /// <value>The information.</value>
        public IterationInfo Info { get; set; }

        /// <summary>
        ///     Gets or sets the instance to populate
        /// </summary>
        /// <value>The instance.</value>
        public object Instance { get; set; }
    }
}