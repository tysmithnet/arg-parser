// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-20-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-20-2018
// ***********************************************************************
// <copyright file="ILexer.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace ArgParser.Core
{
    public interface ILexer
    {
        IEnumerable<IToken> Lex(string[] args);
    }

    public interface ILexer<out T> where T : ILexer, IToken
    {
        IEnumerable<T> Lex(string[] args);
    }
}