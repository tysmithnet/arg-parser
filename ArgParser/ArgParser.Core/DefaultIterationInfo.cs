// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-20-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-21-2018
// ***********************************************************************
// <copyright file="DefaultIterationInfo.cs" company="ArgParser.Core">
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
    ///     Class DefaultIterationInfo.
    /// </summary>
    /// <seealso cref="ArgParser.Core.IIterationInfo" />
    public class DefaultIterationInfo : IIterationInfo
    {
        /// <summary>
        ///     Consumes the specified number tokens.
        /// </summary>
        /// <param name="numTokens">The number tokens.</param>
        /// <returns>IIterationInfo.</returns>
        /// <inheritdoc />
        public IIterationInfo Consume(int numTokens) => SetIndex(Index + numTokens);

        /// <summary>
        ///     Sets the index.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <returns>IIterationInfo.</returns>
        public IIterationInfo SetIndex(int i)
        {
            return Clone(info => info.Index = i);
        }

        /// <summary>
        ///     Sets the tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>IIterationInfo.</returns>
        public IIterationInfo SetTokens(IList<IToken> tokens)
        {
            return Clone(info => info.Tokens = tokens?.ToList());
        }

        /// <summary>
        ///     Clones the specified transformer.
        /// </summary>
        /// <param name="transformer">The transformer.</param>
        /// <returns>DefaultIterationInfo.</returns>
        private DefaultIterationInfo Clone(Action<DefaultIterationInfo> transformer)
        {
            var newGuy = new DefaultIterationInfo
            {
                Args = Args?.ToArray(),
                Tokens = Tokens?.ToList(),
                Index = Index
            };
            transformer?.Invoke(newGuy);
            return newGuy;
        }

        /// <summary>
        ///     Gets or sets the arguments.
        /// </summary>
        /// <value>The arguments.</value>
        /// <inheritdoc />
        public string[] Args { get; set; }

        /// <summary>
        ///     Gets the current.
        /// </summary>
        /// <value>The current.</value>
        /// <inheritdoc />
        public IToken Current => !IsComplete ? Tokens?[Index] : null;

        /// <inheritdoc />
        public IToken First => Tokens?.FirstOrDefault();

        /// <summary>
        ///     Gets or sets the index.
        /// </summary>
        /// <value>The index.</value>
        /// <inheritdoc />
        public int Index { get; set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is complete.
        /// </summary>
        /// <value><c>true</c> if this instance is complete; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsComplete => Index >= Tokens?.Count;

        /// <inheritdoc />
        public bool IsFirst => Index == 0;

        /// <inheritdoc />
        public bool IsInternal => Index > 0 && Index < Tokens?.Count - 1;

        /// <inheritdoc />
        public bool IsLast => Index == Tokens?.Count - 1;

        /// <inheritdoc />
        public IToken Last => Tokens?.LastOrDefault();

        /// <summary>
        ///     Gets the next.
        /// </summary>
        /// <value>The next.</value>
        /// <inheritdoc />
        public IToken Next => Rest?.FirstOrDefault();

        /// <summary>
        ///     Gets the rest.
        /// </summary>
        /// <value>The rest.</value>
        /// <inheritdoc />
        public IEnumerable<IToken> Rest => Tokens?.Skip(Index + 1);

        /// <summary>
        ///     Gets the tokens.
        /// </summary>
        /// <value>The tokens.</value>
        /// <inheritdoc />
        public IReadOnlyList<IToken> Tokens { get; internal set; }
    }
}