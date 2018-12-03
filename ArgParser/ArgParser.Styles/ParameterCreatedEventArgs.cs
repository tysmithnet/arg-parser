// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 12-02-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 12-02-2018
// ***********************************************************************
// <copyright file="ParameterCreatedEventArgs.cs" company="tysmith.net">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    /// Event args for when a new parameter is created for a parser
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ParameterCreatedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterCreatedEventArgs"/> class.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public ParameterCreatedEventArgs(Parameter parameter)
        {
            Parameter = parameter.ThrowIfArgumentNull(nameof(parameter));
        }

        /// <summary>
        /// Gets or sets the parameter.
        /// </summary>
        /// <value>The parameter.</value>
        public Parameter Parameter { get; set; }
    }
}