// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 11-04-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="ConsumptionRequestExtensions.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    /// <summary>
    ///     Convenience methods for retrieving information from ConsumptionRequest instances
    /// </summary>
    public static class ConsumptionRequestExtensions
    {
        /// <summary>
        ///     Returns all args to be consumed
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Args to be consumed</returns>
        public static IEnumerable<string> AllToBeConsumed(this ConsumptionRequest request) =>
            request.Info.Args.Skip(request.Info.Index).Take(request.Max);

        /// <summary>
        ///     Returns the first arg to be consumed
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The first arg to be consumed</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string First(this ConsumptionRequest request)
        {
            var from = request.AllToBeConsumed().ToList();
            if (!from.Any())
                throw new ArgumentOutOfRangeException();
            return from.First();
        }

        /// <summary>
        ///     Determines whether the specified request has a second argument.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns><c>true</c> if the specified request has a second argument; otherwise, <c>false</c>.</returns>
        public static bool HasNext(this ConsumptionRequest request) =>
            request.Info.Index + 1 < request.Info.Index + request.Max;

        /// <summary>
        ///     Gets the last arg allowed to be consumed
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The last arg allowed to be consumed</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string Last(this ConsumptionRequest request)
        {
            var from = request.AllToBeConsumed().ToList();
            if (!from.Any())
                throw new ArgumentOutOfRangeException();
            return from.Last();
        }

        /// <summary>
        ///     Returns the next arg
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The next arg</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Max - Cannot get next arg because it is outside the range of consumable
        ///     values
        /// </exception>
        public static string Next(this ConsumptionRequest request)
        {
            if (request.Max < 2)
                throw new ArgumentOutOfRangeException(nameof(request.Max),
                    "Cannot get next arg because it is outside the range of consumable values");
            return request.Info.Args[request.Info.Index + 1];
        }

        /// <summary>
        ///     Gets the rest of the args allowed to be consumed
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The rest of the args allowed to be consumed</returns>
        public static IEnumerable<string> Rest(this ConsumptionRequest request) =>
            request.Info.Args.Skip(request.Info.Index + 1).Take(request.Max - 1);
    }
}