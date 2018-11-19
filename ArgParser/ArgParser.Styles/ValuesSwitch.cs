// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-12-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="ValuesSwitch.cs" company="ArgParser.Styles">
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
    /// Represents a switch that requires 2 parts: the switch and the list of values
    /// </summary>
    /// <seealso cref="ArgParser.Styles.Switch" />
    public class ValuesSwitch : Switch
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValuesSwitch"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="letter">The letter.</param>
        /// <param name="word">The word.</param>
        /// <param name="consumeCallback">The consume callback.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        public ValuesSwitch(Parser parent, char? letter, string word, Action<object, string[]> consumeCallback,
            int min = 1,
            int max = int.MaxValue) : base(parent, letter, word,
            (o, strings) => consumeCallback(o, strings.Skip(1).ToArray()))
        {
            MinRequired = min;
            MaxAllowed = max == int.MaxValue ? max : max + 1;
        }
    }

    /// <summary>
    /// Represents a switch that requires 2 parts: the switch and the list of values
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ArgParser.Styles.Switch" />
    public class ValuesSwitch<T> : Switch
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValuesSwitch{T}"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="letter">The letter.</param>
        /// <param name="word">The word.</param>
        /// <param name="consumeCallback">The consume callback.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        public ValuesSwitch(Parser parent, char? letter, string word, Action<T, string[]> consumeCallback, int min = 1,
            int max = int.MaxValue) : base(parent, letter, word, consumeCallback.ToNonGenericAction())
        {
            MinRequired = min;
            MaxAllowed = max == int.MaxValue ? max : max + 1;
        }
    }
}