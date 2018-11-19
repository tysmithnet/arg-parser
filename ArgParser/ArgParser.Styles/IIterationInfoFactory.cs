// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="IIterationInfoFactory.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    /// Represents something that is capable of creating iteration info
    /// </summary>
    /// <seealso cref="ArgParser.Styles.IParseStrategyUnit" />
    public interface IIterationInfoFactory : IParseStrategyUnit
    {
        /// <summary>
        /// Creates an IterationInfo from the supplied request
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>IterationInfo.</returns>
        IterationInfo Create(IterationInfoRequest request);
    }
}