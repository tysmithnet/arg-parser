// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="IConsumptionRequestFactory.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    /// Represents something capable of creating a consumption request from the results
    /// of potential consumption identification
    /// </summary>
    /// <seealso cref="ArgParser.Styles.IParseStrategyUnit" />
    public interface IConsumptionRequestFactory : IParseStrategyUnit
    {
        /// <summary>
        /// Creates a request based off the results of potential consumption
        /// </summary>
        /// <param name="consumerResult">The consumer result.</param>
        /// <param name="selected">The selected.</param>
        /// <returns>The request</returns>
        ConsumptionRequest Create(PotentialConsumerResult consumerResult, ConsumptionResult selected);
    }
}