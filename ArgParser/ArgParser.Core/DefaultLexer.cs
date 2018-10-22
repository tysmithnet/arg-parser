// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-20-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-21-2018
// ***********************************************************************
// <copyright file="DefaultLexer.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    /// <summary>
    ///     Class DefaultLexer.
    /// </summary>
    /// <seealso cref="ArgParser.Core.ILexer" />
    public class DefaultLexer : ILexer
    {
        /// <summary>
        ///     Lexes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>IEnumerable&lt;IToken&gt;.</returns>
        /// <inheritdoc />
        public IEnumerable<IToken> Lex(string[] args)
        {
            return args?.Select(a => new Token(a)).ToList().PreventNull();
        }
    }
}