// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-20-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-20-2018
// ***********************************************************************
// <copyright file="IToken.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ArgParser.Core
{
    /// <summary>
    ///     Interface IToken
    /// </summary>
    public interface IToken
    {
        /// <summary>
        ///     Gets the raw.
        /// </summary>
        /// <value>The raw.</value>
        string Raw { get; }
    }
}