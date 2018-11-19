// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="SimpleHelp.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ArgParser.Core
{
    /// <summary>
    ///     Represents basic help
    /// </summary>
    public class SimpleHelp
    {
        /// <summary>
        ///     Gets or sets the name
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the short description. This should be about 12 items or less.
        /// </summary>
        /// <value>The short description.</value>
        public string ShortDescription { get; set; }
    }
}