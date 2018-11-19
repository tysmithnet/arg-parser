// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-12-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="BooleanSwitch.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    ///     Represents a switch that takes 0 additional values
    /// </summary>
    /// <seealso cref="ArgParser.Styles.Switch" />
    public class BooleanSwitch : Switch
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BooleanSwitch" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="letter">The letter.</param>
        /// <param name="word">The word.</param>
        /// <param name="consumeCallback">The consume callback.</param>
        public BooleanSwitch(Parser parent, char? letter, string word, Action<object> consumeCallback) : base(parent,
            letter, word,
            (o, strings) => consumeCallback(o))
        {
            MinRequired = 1;
            MaxAllowed = 1;
        }
    }

    /// <summary>
    ///     Represents a switch that takes 0 additional values
    /// </summary>
    /// <typeparam name="T">The type of the instance this instance can populate</typeparam>
    /// <seealso cref="ArgParser.Styles.BooleanSwitch" />
    public class BooleanSwitch<T> : BooleanSwitch
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BooleanSwitch{T}" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="letter">The letter.</param>
        /// <param name="word">The word.</param>
        /// <param name="consumeCallback">The consume callback.</param>
        public BooleanSwitch(Parser parent, char? letter, string word, Action<T> consumeCallback) : base(parent, letter,
            word,
            consumeCallback.ToNonGenericAction())
        {
        }
    }
}