// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-16-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-17-2018
// ***********************************************************************
// <copyright file="CardinalityError.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ArgParser.Core
{
    /// <summary>
    ///     Represents an error in processing that results from having the incorrect number of values
    /// </summary>
    /// <seealso cref="ArgParser.Core.ParsingError" />
    public class CardinalityError : ParsingError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CardinalityError" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <inheritdoc />
        public CardinalityError(string message) : base(message)
        {
        }
    }
}