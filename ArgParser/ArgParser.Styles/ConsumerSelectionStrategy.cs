// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="ConsumerSelectionStrategy.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    /// Consumer selection strategy that will prioritize switches over positionals
    /// </summary>
    /// <seealso cref="ArgParser.Styles.IConsumerSelectionStrategy" />
    public class ConsumerSelectionStrategy : IConsumerSelectionStrategy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsumerSelectionStrategy"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ConsumerSelectionStrategy(IContext context)
        {
            Context = context.ThrowIfArgumentNull(nameof(context));
        }

        /// <summary>
        /// Selects the specified result.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns>ConsumptionResult.</returns>
        /// <exception cref="ForwardProgressException"></exception>
        public ConsumptionResult Select(PotentialConsumerResult result)
        {
            ConsumptionResult foundResult = null;
            var switchResults = result.ConsumptionResults.Where(r => r.ConsumingParameter is Switch).ToList();
            if (switchResults.Any())
                foundResult = switchResults.FirstOrDefault(r => result.Chain.Contains(r.ConsumingParameter.Parser));

            if (foundResult == null)
                foundResult =
                    result.ConsumptionResults.FirstOrDefault(r => result.Chain.Contains(r.ConsumingParameter.Parser)); // todo: why first? why not by most args or least args consumed

            if (foundResult == null)
                throw new ForwardProgressException(result.Info);
            return foundResult;
        }

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        public IContext Context { get; set; }
    }
}