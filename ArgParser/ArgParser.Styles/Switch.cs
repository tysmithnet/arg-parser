// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-12-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="Switch.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    ///     Represents a parameter that has a text indicator that some number of proceeding
    ///     args are of some significance. For example, -h, --value
    /// </summary>
    /// <seealso cref="ArgParser.Core.Parameter" />
    /// <seealso cref="ArgParser.Styles.IRequirable" />
    public abstract class Switch : Parameter, IRequirable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Switch" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="letter">The letter.</param>
        /// <param name="word">The word.</param>
        /// <param name="consumeCallback">The consume callback.</param>
        /// <exception cref="ArgumentException">Letter or word must be provided</exception>
        protected Switch(Parser parent, char? letter, string word, Action<object, string[]> consumeCallback) : base(
            parent, consumeCallback)
        {
            if (letter == null && word == null)
                throw new ArgumentException($"You must either provide a letter or a word to identify this switch");
            Letter = letter;
            Word = word;
        }

        /// <summary>
        ///     Determines whether this parameter can consume the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="info">The information.</param>
        /// <returns>The result of the consumption</returns>
        public override ConsumptionResult CanConsume(object instance, IterationInfo info)
        {
            if (!IsLetterMatch(info) && !IsWordMatch(info)) return new ConsumptionResult(info, 0, null);
            var canBeTaken = info.FromNowOn().ToList();
            if (canBeTaken.Count < MinRequired)
                return new ConsumptionResult(info, 0, null);
            var actuallyTaken = canBeTaken.Take(MaxAllowed).ToList();
            return new ConsumptionResult(info, actuallyTaken.Count, this);
        }

        /// <summary>
        ///     Determines whether the letter matches the info.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <returns><c>true</c> if [is letter match] [the specified information]; otherwise, <c>false</c>.</returns>
        public virtual bool IsLetterMatch(IterationInfo info) => Letter.HasValue && info.Current == $"-{Letter}";

        /// <summary>
        ///     Determines whether the word matches the info
        /// </summary>
        /// <param name="info">The information.</param>
        /// <returns><c>true</c> if [is word match] [the specified information]; otherwise, <c>false</c>.</returns>
        public virtual bool IsWordMatch(IterationInfo info) => Word != null && info.Current == $"--{Word}";

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            if (Letter != null && Word != null)
                return $"-{Letter}, --{Word}";
            return Letter.HasValue ? $"-{Letter}" : $"--{Word}";
        }

        /// <summary>
        ///     Gets a value indicating whether this instance is required.
        /// </summary>
        /// <value><c>true</c> if this instance is required; otherwise, <c>false</c>.</value>
        public bool IsRequired { get; set; }

        /// <summary>
        ///     Gets or sets the letter.
        /// </summary>
        /// <value>The letter.</value>
        public char? Letter { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the word.
        /// </summary>
        /// <value>The word.</value>
        public string Word { get; protected internal set; }
    }
}