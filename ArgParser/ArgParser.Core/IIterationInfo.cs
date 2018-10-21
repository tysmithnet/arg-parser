// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-20-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-20-2018
// ***********************************************************************
// <copyright file="IIterationInfo.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace ArgParser.Core
{
    /// <summary>
    ///     Interface IIterationInfo
    /// </summary>
    public interface IIterationInfo
    {
        /// <summary>
        ///     Consumes the specified number tokens.
        /// </summary>
        /// <param name="numTokens">The number tokens.</param>
        /// <returns>IIterationInfo.</returns>
        IIterationInfo Consume(int numTokens);

        /// <summary>
        ///     Gets the arguments.
        /// </summary>
        /// <value>The arguments.</value>
        string[] Args { get; }

        /// <summary>
        ///     Gets the current.
        /// </summary>
        /// <value>The current.</value>
        IToken Current { get; }

        /// <summary>
        ///     Gets the index.
        /// </summary>
        /// <value>The index.</value>
        int Index { get; }

        /// <summary>
        ///     Gets a value indicating whether this instance is complete.
        /// </summary>
        /// <value><c>true</c> if this instance is complete; otherwise, <c>false</c>.</value>
        bool IsComplete { get; }

        /// <summary>
        ///     Gets the next.
        /// </summary>
        /// <value>The next.</value>
        IToken Next { get; }

        /// <summary>
        ///     Gets the rest.
        /// </summary>
        /// <value>The rest.</value>
        IEnumerable<IToken> Rest { get; }

        /// <summary>
        ///     Gets the tokens.
        /// </summary>
        /// <value>The tokens.</value>
        IReadOnlyList<IToken> Tokens { get; }

        bool IsLast { get; }

        bool IsFirst { get; }

        bool IsInternal { get; }

        IToken Last { get; }
        IToken First { get; }
    }
}