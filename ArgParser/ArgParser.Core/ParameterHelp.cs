// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="ParameterHelp.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ArgParser.Core
{
    /// <summary>
    ///     Represents generic help for a parameter
    /// </summary>
    /// <seealso cref="ArgParser.Core.SimpleHelp" />
    public class ParameterHelp : SimpleHelp
    {
        /// <summary>
        ///     Gets or sets the default value text to appear in help documenation as the default value
        /// </summary>
        /// <value>The default value.</value>
        public string DefaultValue { get; set; }

        /// <summary>
        ///     Gets or sets string that will appear in help documentation to take the place of user input
        /// </summary>
        /// <value>The value alias.</value>
        public string ValueAlias { get; set; }
    }
}