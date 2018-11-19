// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 11-04-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="IParseStrategy.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace ArgParser.Core
{
    /// <summary>
    /// Represents an object that knows how to parse arguments
    /// </summary>
    public interface IParseStrategy
    {
        /// <summary>
        /// Parses the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The result of the parsing</returns>
        IParseResult Parse(string[] args);
    }
}