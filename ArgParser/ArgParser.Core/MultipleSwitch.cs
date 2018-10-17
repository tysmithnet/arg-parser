// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-15-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-16-2018
// ***********************************************************************
// <copyright file="MultipleSwitch.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace ArgParser.Core
{
    /// <summary>
    ///     Class MultipleSwitch.
    /// </summary>
    /// <typeparam name="TOptions">The type of the t options.</typeparam>
    /// <seealso cref="ArgParser.Core.ValueSwitch{TOptions}" />
    public class MultipleSwitch<TOptions> : ValueSwitch<TOptions> where TOptions : IOptions
    {
        public int? Min { get; set; }
        public int? Max { get; set; }

        /// <summary>
        ///     Gets or sets the transformer.
        /// </summary>
        /// <value>The transformer.</value>
        public Action<TOptions, string[]> Transformer { get; set; }
    }
}