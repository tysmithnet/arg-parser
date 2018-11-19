// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 11-05-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="NoFactoryFunctionException.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ArgParser.Core
{
    /// <summary>
    ///     An exception that arises when a parser is needed and it does not have a factory function
    /// </summary>
    /// <seealso cref="ArgParser.Core.ParseException" />
    public class NoFactoryFunctionException : ParseException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NoFactoryFunctionException" /> class.
        /// </summary>
        /// <param name="parser">The parser.</param>
        public NoFactoryFunctionException(Parser parser) : base($"No factory function set on parser={parser.Id}")
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