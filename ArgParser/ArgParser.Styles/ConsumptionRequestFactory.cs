// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="ConsumptionRequestFactory.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    ///     Consumption request factory that will limit the number consumed to the next identifiable consumer
    /// </summary>
    /// <seealso cref="ArgParser.Styles.IConsumptionRequestFactory" />
    public class ConsumptionRequestFactory : IConsumptionRequestFactory
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ConsumptionRequestFactory" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ConsumptionRequestFactory(IContext context)
        {
            Context = context.ThrowIfArgumentNull(nameof(context));
        }

        /// <summary>
        ///     Creates a request based off the results of potential consumption
        /// </summary>
        /// <param name="consumerResult">The consumer result.</param>
        /// <param name="selected">The selected.</param>
        /// <returns>The request</returns>
        public ConsumptionRequest Create(PotentialConsumerResult consumerResult, ConsumptionResult selected)
        {
            for (var i = 1;
                i < selected.NumConsumed;
                i++)
                foreach (var parser in consumerResult.Chain)
                {
                    var info = consumerResult.Info.Consume(i);
                    var res = parser.CanConsume(selected, info);
                    if (res.Info > info && res.ConsumingParameter != null && res.ConsumingParameter is Switch)
                        return new ConsumptionRequest(consumerResult.Info, i);
                }

            return new ConsumptionRequest(consumerResult.Info, selected.NumConsumed);
        }

        /// <summary>
        ///     Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        public IContext Context { get; set; }
    }
}