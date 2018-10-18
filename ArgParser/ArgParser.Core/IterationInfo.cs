// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-16-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-17-2018
// ***********************************************************************
// <copyright file="IterationInfo.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ArgParser.Core
{
    /// <summary>
    ///     Represents the current state of the iteration of the arguments
    /// </summary>
    [DebuggerDisplay("{Index}:{Cur}")]
    public class IterationInfo : IIterationInfo
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="IterationInfo" /> class.
        /// </summary>
        /// <param name="allArgs">All arguments passed to the program.</param>
        /// <param name="index">The index of the read head.</param>
        /// <exception cref="System.ArgumentNullException">allArgs</exception>
        /// <inheritdoc />
        public IterationInfo(string[] allArgs, int index)
        {
            AllArgs = allArgs ?? throw new ArgumentNullException(nameof(allArgs));
            Index = index;
        }

        /// <inheritdoc />
        public void AddError(ParsingError error)
        {
            Errors.Add(error);
        }

        /// <summary>
        ///     Clones this instance.
        /// </summary>
        /// <returns>IterationInfo.</returns>
        public IIterationInfo Clone() => new IterationInfo(AllArgs, Index)
        {
            Errors = new List<ParsingError>(Errors)
        };

        /// <summary>
        ///     Gets all arguments.
        /// </summary>
        /// <value>All arguments.</value>
        public string[] AllArgs { get; internal set; }

        /// <summary>
        ///     Gets the current token.
        /// </summary>
        /// <value>The current.</value>
        public string Cur => AllArgs[Index];

        /// <summary>
        ///     Gets all tokens from the current token onward
        /// </summary>
        /// <value>The current on.</value>
        public string[] CurOn => AllArgs.Skip(Index).ToArray();

        /// <inheritdoc />
        public bool HasErrors => Errors.Any();

        /// <summary>
        ///     Gets the index.
        /// </summary>
        /// <value>The index.</value>
        public int Index { get; set; }

        /// <summary>
        ///     Gets a value indicating whether the iteration has ended
        /// </summary>
        /// <value><c>true</c> if this instance is end; otherwise, <c>false</c>.</value>
        public bool IsEnd => Index >= AllArgs.Length;

        /// <summary>
        ///     Gets the rest of the tokens following the current one
        /// </summary>
        /// <value>The rest.</value>
        public string[] Rest => AllArgs.Skip(Index + 1).ToArray();

        /// <summary>
        ///     Gets or sets the errors.
        /// </summary>
        /// <value>The errors.</value>
        internal IList<ParsingError> Errors { get; set; } = new List<ParsingError>();
    }
}