// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-17-2018
// ***********************************************************************
// <copyright file="IParsingStrategy.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace ArgParser.Core
{
    /// <summary>
    /// Interface IParsingStrategy
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IParsingStrategy<T>
    {
        /// <summary>
        /// Resets this instance.
        /// </summary>
        void Reset();
    }
}