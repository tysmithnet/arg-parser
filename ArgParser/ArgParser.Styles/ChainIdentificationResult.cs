// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="ChainIdentificationResult.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    ///     Represents the result of a parser chain identification
    /// </summary>
    public class ChainIdentificationResult
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ChainIdentificationResult" /> class.
        /// </summary>
        /// <param name="chain">The chain.</param>
        /// <param name="consumedArgs">The consumed arguments.</param>
        public ChainIdentificationResult(IEnumerable<Parser> chain, string[] consumedArgs)
        {
            Chain = chain.ThrowIfArgumentNull(nameof(chain)).ToList();
            ConsumedArgs = consumedArgs.ThrowIfArgumentNull(nameof(consumedArgs));
        }

        /// <summary>
        ///     Gets or sets the chain.
        /// </summary>
        /// <value>The chain.</value>
        public IList<Parser> Chain { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the consumed arguments.
        /// </summary>
        /// <value>The consumed arguments.</value>
        public string[] ConsumedArgs { get; protected internal set; }

        /// <summary>
        ///     Gets the identified parser.
        /// </summary>
        /// <value>The identified parser.</value>
        public Parser IdentifiedParser => Chain.Last();
    }
}