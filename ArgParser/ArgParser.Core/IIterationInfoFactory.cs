// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-20-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-20-2018
// ***********************************************************************
// <copyright file="IIterationInfoFactory.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ArgParser.Core
{
    /// <summary>
    ///     Interface IIterationInfoFactory
    /// </summary>
    public interface IIterationInfoFactory
    {
        /// <summary>
        ///     Creates the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>IIterationInfo.</returns>
        IIterationInfo Create(string[] args);
    }
}