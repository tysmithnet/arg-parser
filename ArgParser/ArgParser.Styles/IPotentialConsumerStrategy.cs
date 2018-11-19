// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="IPotentialConsumerStrategy.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ArgParser.Styles
{
    /// <summary>
    ///     Interface IPotentialConsumerStrategy
    /// </summary>
    /// <seealso cref="ArgParser.Styles.IParseStrategyUnit" />
    public interface IPotentialConsumerStrategy : IParseStrategyUnit
    {
        /// <summary>
        ///     Identifies the potential consumer.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>PotentialConsumerResult.</returns>
        PotentialConsumerResult IdentifyPotentialConsumer(PotentialConsumerRequest request);
    }
}