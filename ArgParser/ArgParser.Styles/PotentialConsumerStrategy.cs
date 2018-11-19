// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="PotentialConsumerStrategy.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    /// IPotentialConsumerStrategy that choose based on whether any args were consumed
    /// </summary>
    /// <seealso cref="ArgParser.Styles.IPotentialConsumerStrategy" />
    public class PotentialConsumerStrategy : IPotentialConsumerStrategy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PotentialConsumerStrategy"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public PotentialConsumerStrategy(IContext context)
        {
            Context = context.ThrowIfArgumentNull(nameof(context));
        }

        /// <summary>
        /// Identifies the potential consumer.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>PotentialConsumerResult.</returns>
        public PotentialConsumerResult IdentifyPotentialConsumer(PotentialConsumerRequest request)
        {
            var consumptionResultsForTheParsersWhoCanConsume = request.ChainIdentificationResult.Chain
                .Select(x => x.CanConsume(request.Instance, request.Info))
                .Where(x => x.NumConsumed > 0).ToList();
            return new PotentialConsumerResult(request.ChainIdentificationResult.Chain,consumptionResultsForTheParsersWhoCanConsume,request.Info);
        }

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        public IContext Context { get; set; }
    }
}