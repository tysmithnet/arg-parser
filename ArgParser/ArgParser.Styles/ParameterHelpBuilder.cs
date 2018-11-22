// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-12-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="ParameterHelpBuilder.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    ///     Builder pattern for ParameterHelp
    /// </summary>
    public class ParameterHelpBuilder
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ParameterHelpBuilder" /> class.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public  ParameterHelpBuilder(Parameter parameter)
        {
            Parameter = parameter.ThrowIfArgumentNull(nameof(parameter));
        }

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>ParameterHelp.</returns>
        public virtual ParameterHelp Build() => Help;

        /// <summary>
        ///     Sets the default value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>ParameterHelpBuilder.</returns>
        public virtual ParameterHelpBuilder SetDefaultValue(string value)
        {
            Help.DefaultValue = value;
            return this;
        }

        /// <summary>
        ///     Sets the name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>ParameterHelpBuilder.</returns>
        public virtual ParameterHelpBuilder SetName(string name)
        {
            Help.Name = name;
            return this;
        }

        /// <summary>
        ///     Sets the short description.
        /// </summary>
        /// <param name="desc">The desc.</param>
        /// <returns>ParameterHelpBuilder.</returns>
        public virtual ParameterHelpBuilder SetShortDescription(string desc)
        {
            Help.ShortDescription = desc;
            return this;
        }

        /// <summary>
        ///     Sets the value alias.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>ParameterHelpBuilder.</returns>
        public virtual ParameterHelpBuilder SetValueAlias(string alias)
        {
            Help.ValueAlias = alias;
            return this;
        }

        /// <summary>
        ///     Gets or sets the help.
        /// </summary>
        /// <value>The help.</value>
        protected internal ParameterHelp Help { get; set; } = new ParameterHelp();

        /// <summary>
        ///     Gets or sets the parameter.
        /// </summary>
        /// <value>The parameter.</value>
        protected internal Parameter Parameter { get; set; }
    }
}