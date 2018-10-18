// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-16-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-17-2018
// ***********************************************************************
// <copyright file="Switch.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace ArgParser.Core
{
    /// <summary>
    /// Represents a token that signals that zero or more of the tokens that follow are to be consumed by the switch
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="PipelineElement{T}" />
    public class Switch<T> : PipelineElement<T>
    {
        /// <summary>
        /// Gets or sets the letter that will identify this switch in a group. A group is a single token that represents multiple switches.
        /// </summary>
        /// <value>The group letter.</value>
        public char? GroupLetter { get; set; }
        /// <summary>
        /// Gets or sets functionality that identifies whether the current token is a switch or not
        /// </summary>
        /// <value>The is token.</value>
        public Func<IIterationInfo, bool> IsToken { get; set; }
    }
}