// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="ParserHelp.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ArgParser.Core
{
    /// <summary>
    ///     Represents generic help for a parser
    /// </summary>
    /// <seealso cref="ArgParser.Core.FullHelp" />
    public class ParserHelp : FullHelp
    {
        /// <summary>
        ///     Gets or sets the author.
        /// </summary>
        /// <value>The author.</value>
        public string Author { get; set; }

        /// <summary>
        ///     Gets or sets the repository URL.
        /// </summary>
        /// <value>The repository URL.</value>
        public string RepositoryUrl { get; set; }

        /// <summary>
        ///     Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public string Version { get; set; }
    }
}