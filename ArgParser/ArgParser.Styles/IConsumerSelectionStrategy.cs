// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="IConsumerSelectionStrategy.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    ///     Represents an object that is capable of selecting a potential consumption
    ///     result out of a field of potentially many consumption results.
    /// </summary>
    /// <seealso cref="ArgParser.Styles.IParseStrategyUnit" />
    public interface IConsumerSelectionStrategy : IParseStrategyUnit
    {
        /// <summary>
        ///     Selects the specified result.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns>ConsumptionResult.</returns>
        ConsumptionResult Select(PotentialConsumerResult result);
    }
}