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

using System;
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

    public class DefaultLexer<T> : DefaultLexer, ILexer<T> where T : IToken
    {
        public Func<string, T> FactoryFunc { get; set; }

        /// <inheritdoc />
        public DefaultLexer(Func<string, T> factoryFunc)
        {
            FactoryFunc = factoryFunc ?? throw new ArgumentNullException(nameof(factoryFunc));
        }

        public new IEnumerable<T> Lex(string[] args)
        {
            return args?.Select(x => FactoryFunc(x)).PreventNull();
        }
    }
}