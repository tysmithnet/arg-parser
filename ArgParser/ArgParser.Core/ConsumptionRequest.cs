// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 11-04-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="ConsumptionRequest.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ArgParser.Core
{
    /// <summary>
    ///     A request to consume arguments
    /// </summary>
    public class ConsumptionRequest
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ConsumptionRequest" /> class.
        /// </summary>
        /// <param name="info">The iteration info for where to start consuming.</param>
        /// <param name="max">The maximum number of arguments to be consumed.</param>
        public ConsumptionRequest(IterationInfo info, int max = 1)
        {
            Info = info.ThrowIfArgumentNull(nameof(info));
            Max = max;
        }

        /// <summary>
        ///     Gets or sets the iteration information.
        /// </summary>
        /// <value>The information.</value>
        public IterationInfo Info { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the maximum number of arguments to be consumed.
        /// </summary>
        /// <value>The maximum.</value>
        public int Max { get; protected internal set; }
    }
}