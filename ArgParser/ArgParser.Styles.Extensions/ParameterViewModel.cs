﻿// ***********************************************************************
// Assembly         : ArgParser.Styles.Extensions
// Author           : @tysmithnet
// Created          : 11-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-19-2018
// ***********************************************************************
// <copyright file="ParameterViewModel.cs" company="ArgParser.Styles.Extensions">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Alba.CsConsoleFormat;
using ArgParser.Core;

namespace ArgParser.Styles.Extensions
{
    /// <summary>
    ///     View model for Parameters
    /// </summary>
    public class ParameterViewModel
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ParameterViewModel" /> class.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="theme">The theme.</param>
        public ParameterViewModel(Parameter parameter, Theme theme)
        {
            Parameter = parameter.ThrowIfArgumentNull(nameof(parameter));
            Theme = theme.ThrowIfArgumentNull(nameof(theme));
        }

        /// <summary>
        ///     Gets or sets the line thickness.
        /// </summary>
        /// <value>The line thickness.</value>
        public LineThickness LineThickness { get; set; } = new LineThickness(LineWidth.Single, LineWidth.Single);

        /// <summary>
        ///     Gets or sets the parameter.
        /// </summary>
        /// <value>The parameter.</value>
        public Parameter Parameter { get; protected internal set; }

        /// <summary>
        ///     Gets the required text.
        /// </summary>
        /// <value>The required text.</value>
        public string RequiredText
        {
            get
            {
                if (Parameter is IRequirable casted && casted.IsRequired)
                    return "✓";
                return "";
            }
        }

        /// <summary>
        ///     Gets or sets the theme.
        /// </summary>
        /// <value>The theme.</value>
        public Theme Theme { get; protected internal set; }
    }
}