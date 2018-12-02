// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-28-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-28-2018
// ***********************************************************************
// <copyright file="SeparatedSwitch.cs" company="tysmith.net">
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
    ///     Represents a switch where the identifier is separated from its value by
    ///     a token.
    /// </summary>
    /// <seealso cref="ArgParser.Styles.Switch" />
    public class SeparatedSwitch : Switch
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SeparatedSwitch" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="letter">The letter.</param>
        /// <param name="word">The word.</param>
        /// <param name="consumeCallback">The consume callback.</param>
        public SeparatedSwitch(Parser parent, char? letter, string word, Action<object, string> consumeCallback) : base(
            parent, letter, word, Convert(consumeCallback))
        {
            MinRequired = 1;
            MaxAllowed = 1;
        }

        /// <summary>
        ///     Determines whether this parameter can consume the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="info">The information.</param>
        /// <returns>The result of the consumption</returns>
        public override ConsumptionResult CanConsume(object instance, IterationInfo info)
        {
            if (Letter.HasValue && info.Current.StartsWith($"{LetterToken}{Letter}{Separator}"))
                return new ConsumptionResult(info, 1, this);
            if (Word.IsNotNullOrWhiteSpace() && info.Current.StartsWith($"{WordToken}{Word}{Separator}"))
                return new ConsumptionResult(info, 1, this);
            return new ConsumptionResult(info, 0, this);
        }

        /// <summary>
        ///     Consumes the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="request">The request.</param>
        /// <returns>ConsumptionResult.</returns>
        public override ConsumptionResult Consume(object instance, ConsumptionRequest request)
        {
            var index = request.Info.Current.IndexOf(Separator, StringComparison.Ordinal);
            var value = request.Info.Current.Substring(index + 1);
            ConsumeCallback(instance, new[] {value});
            return new ConsumptionResult(request.Info, 1, this);
        }

        /// <summary>
        ///     Converts the specified callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Action&lt;System.Object, System.String[]&gt;.</returns>
        private static Action<object, string[]> Convert(Action<object, string> callback)
        {
            return (o, s) => { callback(o, s.First()); };
        }

        /// <summary>
        ///     Gets or sets the separator.
        /// </summary>
        /// <value>The separator.</value>
        public string Separator { get; set; } = "=";
    }

    /// <summary>
    ///     Class SeparatedSwitch.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ArgParser.Styles.SeparatedSwitch" />
    public class SeparatedSwitch<T> : SeparatedSwitch
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SeparatedSwitch{T}" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="letter">The letter.</param>
        /// <param name="word">The word.</param>
        /// <param name="consumeCallback">The consume callback.</param>
        public SeparatedSwitch(Parser parent, char? letter, string word, Action<T, string> consumeCallback) : base(
            parent, letter, word, consumeCallback.ToNonGenericAction())
        {
        }
    }
}