// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-16-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-17-2018
// ***********************************************************************
// <copyright file="FormatError.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace ArgParser.Core
{
    /// <summary>
    /// Represents a parsing error that occurs when a string is not in the appropriate format
    /// </summary>
    /// <seealso cref="ArgParser.Core.ParsingError" />
    public class FormatError : ParsingError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormatError"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <inheritdoc />
        public FormatError(string message) : base(message)
        {
        }
    }
}