// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-12-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="ParserHelpBuilder.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    ///     Builder pattern for ParserHelp
    /// </summary>
    public class ParserHelpBuilder
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ParserHelpBuilder" /> class.
        /// </summary>
        /// <param name="parser">The parser.</param>
        public ParserHelpBuilder(Parser parser)
        {
            Parser = parser.ThrowIfArgumentNull(nameof(parser));
        }

        /// <summary>
        ///     Adds the example.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="usage">The usage.</param>
        /// <param name="result">The result.</param>
        /// <returns>ParserHelpBuilder.</returns>
        public ParserHelpBuilder AddExample(string name, string description, string usage, string result)
        {
            Help.AddExample(new Example
            {
                Name = name,
                ShortDescription = description,
                Usage = usage,
                Result = result
            });
            return this;
        }

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>ParserHelp.</returns>
        public ParserHelp Build() => Help;

        /// <summary>
        ///     Sets the long description.
        /// </summary>
        /// <param name="desc">The desc.</param>
        /// <returns>ParserHelpBuilder.</returns>
        public ParserHelpBuilder SetLongDescription(string desc)
        {
            Help.LongDescription = desc;
            return this;
        }

        /// <summary>
        ///     Sets the name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>ParserHelpBuilder.</returns>
        public ParserHelpBuilder SetName(string name)
        {
            Help.Name = name;
            return this;
        }

        /// <summary>
        ///     Sets the short description.
        /// </summary>
        /// <param name="desc">The desc.</param>
        /// <returns>ParserHelpBuilder.</returns>
        public ParserHelpBuilder SetShortDescription(string desc)
        {
            Help.ShortDescription = desc;
            return this;
        }

        /// <summary>
        ///     Sets the version.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>ParserHelpBuilder.</returns>
        public ParserHelpBuilder SetVersion(string version)
        {
            Help.Version = version;
            return this;
        }

        /// <summary>
        ///     Gets or sets the help.
        /// </summary>
        /// <value>The help.</value>
        protected internal ParserHelp Help { get; set; } = new ParserHelp();

        /// <summary>
        ///     Gets or sets the parser.
        /// </summary>
        /// <value>The parser.</value>
        protected internal Parser Parser { get; set; }
    }
}