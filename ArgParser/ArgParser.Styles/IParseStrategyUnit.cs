// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="IParseStrategyUnit.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    ///     The base abstraction for all sub components of the ParseStrategy instance
    /// </summary>
    public interface IParseStrategyUnit
    {
        /// <summary>
        ///     Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        IContext Context { get; set; }
    }
}