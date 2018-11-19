// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="IParserChainIdentificationStrategy.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace ArgParser.Styles
{
    /// <summary>
    /// Represents something that is capable of identifying the parsers needed to process the arguments
    /// </summary>
    /// <seealso cref="ArgParser.Styles.IParseStrategyUnit" />
    public interface IParserChainIdentificationStrategy : IParseStrategyUnit
    {
        /// <summary>
        /// Identifies the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>ChainIdentificationResult.</returns>
        ChainIdentificationResult Identify(ChainIdentificationRequest request);
    }
}