// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 11-04-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="ConsumptionResult.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace ArgParser.Core
{
    /// <summary>
    ///     Represents a the result of a consumer's consumption process.
    /// </summary>
    public class ConsumptionResult
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ConsumptionResult" /> class.
        /// </summary>
        /// <param name="parseExceptions">The parse exceptions.</param>
        public ConsumptionResult(params ParseException[] parseExceptions)
        {
            foreach (var parseException in parseExceptions.PreventNull()) ParseExceptions.Add(parseException);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConsumptionResult" /> class.
        /// </summary>
        /// <param name="originalInfo">The original information.</param>
        /// <param name="numConsumed">The number consumed.</param>
        /// <param name="consumingParameter">The consuming parameter.</param>
        public ConsumptionResult(IterationInfo originalInfo, int numConsumed, Parameter consumingParameter)
        {
            NumConsumed = numConsumed;
            Info = originalInfo.Consume(numConsumed);

            // can be null, means there was no consumption
            ConsumingParameter = consumingParameter;
        }

        /// <summary>
        ///     Gets or sets the consuming parameter.
        /// </summary>
        /// <value>The consuming parameter.</value>
        public Parameter ConsumingParameter { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the iteration information AFTER the consumption
        /// </summary>
        /// <value>The information.</value>
        public IterationInfo Info { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the number of args consumed during the process
        /// </summary>
        /// <value>The number consumed.</value>
        public int NumConsumed { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the parse exceptions that occurred during processing
        /// </summary>
        /// <value>The parse exceptions.</value>
        public IList<ParseException> ParseExceptions { get; protected internal set; } = new List<ParseException>();
    }
}