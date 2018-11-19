// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="PotentialConsumerResult.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    /// Represents the result of a request to consume. This happens when a strategy is
    /// trying to determine who is going to be chosen to parse the next set of args.
    /// </summary>
    public class PotentialConsumerResult
    {
        /// <summary>
        /// Gets or sets the chain.
        /// </summary>
        /// <value>The chain.</value>
        public IList<Parser> Chain { get; protected internal set; }
        /// <summary>
        /// Gets or sets the consumption results.
        /// </summary>
        /// <value>The consumption results.</value>
        public IList<ConsumptionResult> ConsumptionResults { get; protected internal set; }
        /// <summary>
        /// Gets or sets the information.
        /// </summary>
        /// <value>The information.</value>
        public IterationInfo Info { get; protected internal set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="PotentialConsumerResult"/> is success.
        /// </summary>
        /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
        public bool Success => ConsumptionResults.Any(x => x.NumConsumed> 0);

        /// <summary>
        /// Initializes a new instance of the <see cref="PotentialConsumerResult"/> class.
        /// </summary>
        /// <param name="chain">The chain.</param>
        /// <param name="consumptionResults">The consumption results.</param>
        /// <param name="info">The information.</param>
        public PotentialConsumerResult(IEnumerable<Parser> chain, IEnumerable<ConsumptionResult> consumptionResults,
            IterationInfo info)
        {
            Chain = chain.ThrowIfArgumentNull(nameof(chain)).ToList();
            ConsumptionResults = consumptionResults.ThrowIfArgumentNull(nameof(consumptionResults)).ToList();
            Info = info.ThrowIfArgumentNull(nameof(info));
        }
    }
}