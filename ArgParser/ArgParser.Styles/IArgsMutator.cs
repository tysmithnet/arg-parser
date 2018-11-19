// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="IArgsMutator.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace ArgParser.Styles
{
    /// <summary>
    /// Represents an object that is capable of mutating arguments prior to consumption
    /// </summary>
    /// <seealso cref="ArgParser.Styles.IParseStrategyUnit" />
    public interface IArgsMutator : IParseStrategyUnit
    {
        /// <summary>
        /// Mutates the arguments based on the request
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Mutated arguments</returns>
        string[] Mutate(MutateArgsRequest request);
    }
}