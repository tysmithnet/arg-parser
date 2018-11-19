// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-12-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-12-2018
// ***********************************************************************
// <copyright file="SingleValueSwitch.cs" company="ArgParser.Styles">
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
    ///     Represents a switch that requires 2 parts: the switch and the value
    ///     e.g. git commit -m somemessage
    /// </summary>
    /// <seealso cref="ArgParser.Styles.Switch" />
    public class SingleValueSwitch : Switch
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SingleValueSwitch" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="letter">The letter.</param>
        /// <param name="word">The word.</param>
        /// <param name="consumeCallback">The consume callback.</param>
        public SingleValueSwitch(Parser parent, char? letter, string word, Action<object, string> consumeCallback) :
            base(parent, letter, word,
                (o, strings) => consumeCallback(o, strings.Skip(1).First()))
        {
            MinRequired = 2;
            MaxAllowed = 2;
        }
    }

    /// <summary>
    ///     Represents a switch that requires 2 parts: the switch and the value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ArgParser.Styles.SingleValueSwitch" />
    public class SingleValueSwitch<T> : SingleValueSwitch
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SingleValueSwitch{T}" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="letter">The letter.</param>
        /// <param name="word">The word.</param>
        /// <param name="consumeCallback">The consume callback.</param>
        public SingleValueSwitch(Parser parent, char? letter, string word, Action<T, string> consumeCallback) : base(
            parent, letter, word,
            Convert(consumeCallback))
        {
        }

        /// <summary>
        ///     Converts the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>Action&lt;System.Object, System.String&gt;.</returns>
        private static Action<object, string> Convert(Action<T, string> action)
        {
            return (instance, s) =>
            {
                if (instance is T casted)
                    action(casted, s);
                else
                    throw new ArgumentException(
                        $"Expected to find object of type={typeof(T).FullName}, but found type={instance.GetType().FullName}");
            };
        }
    }
}