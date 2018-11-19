// ***********************************************************************
// Assembly         : ArgParser.Styles.Alba
// Author           : @tysmithnet
// Created          : 11-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-19-2018
// ***********************************************************************
// <copyright file="ParameterViewModel.cs" company="ArgParser.Styles.Alba">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using ArgParser.Core;

namespace ArgParser.Styles.Alba
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