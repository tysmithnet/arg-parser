// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-20-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-21-2018
// ***********************************************************************
// <copyright file="DefaultIterationInfoFactory.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Linq;

namespace ArgParser.Core
{
    /// <summary>
    ///     Class DefaultIterationInfoFactory.
    /// </summary>
    /// <seealso cref="ArgParser.Core.IIterationInfoFactory" />
    public class DefaultIterationInfoFactory : IIterationInfoFactory
    {
        /// <summary>
        ///     Creates the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>IIterationInfo.</returns>
        /// <inheritdoc />
        public virtual IIterationInfo Create(string[] args)
        {
            var tokens = Lexer.Lex(args);
            return new DefaultIterationInfo
            {
                Tokens = tokens.ToList(),
                Index = 0,
                Args = args.ToArray()
            };
        }

        /// <summary>
        ///     Gets or sets the lexer.
        /// </summary>
        /// <value>The lexer.</value>
        protected internal ILexer Lexer { get; set; } = new DefaultLexer();
    }
}