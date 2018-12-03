// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 11-06-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="MissingValueException.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ArgParser.Core
{
    /// <summary>
    ///     Represents a scenario in which an argument was encountered that could not be processed
    ///     by any consumer
    /// </summary>
    /// <seealso cref="ArgParser.Core.ParseException" />
    public class MissingValueException : ParseException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MissingValueException" /> class.
        /// </summary>
        /// <param name="parser">The parser.</param>
        /// <param name="message">The message.</param>
        public MissingValueException(Parser parser, string message) : base(message)
        {
            Parser = parser.ThrowIfArgumentNull(nameof(parser));
        }

        /// <summary>
        ///     Gets or sets the parser.
        /// </summary>
        /// <value>The parser.</value>
        public Parser Parser { get; protected internal set; }
    }
}