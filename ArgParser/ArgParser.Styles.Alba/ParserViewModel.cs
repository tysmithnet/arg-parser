// ***********************************************************************
// Assembly         : ArgParser.Styles.Alba
// Author           : @tysmithnet
// Created          : 11-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-19-2018
// ***********************************************************************
// <copyright file="ParserViewModel.cs" company="ArgParser.Styles.Alba">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Alba.CsConsoleFormat;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    /// <summary>
    ///     View model for Parsers
    /// </summary>
    public class ParserViewModel
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ParserViewModel" /> class.
        /// </summary>
        /// <param name="parser">The parser.</param>
        /// <param name="theme">The theme.</param>
        /// <exception cref="ArgumentNullException">
        ///     parser
        ///     or
        ///     theme
        /// </exception>
        public ParserViewModel(Parser parser, Theme theme)
        {
            Parser = parser.ThrowIfArgumentNull(nameof(parser));
            Theme = theme.ThrowIfArgumentNull(nameof(theme));
        }

        /// <summary>
        ///     Gets or sets the alias.
        /// </summary>
        /// <value>The alias.</value>
        public string Alias { get; protected internal set; }

        /// <summary>
        ///     Gets the display string.
        /// </summary>
        /// <value>The display string.</value>
        public string DisplayString => Alias.IsNullOrWhiteSpace() ? Parser.Id : Alias;

        public LineThickness LineThickness { get; set; } = new LineThickness(LineWidth.Single, LineWidth.Single);

        /// <summary>
        ///     Gets or sets the parser.
        /// </summary>
        /// <value>The parser.</value>
        public Parser Parser { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the theme.
        /// </summary>
        /// <value>The theme.</value>
        public Theme Theme { get; protected internal set; }
    }
}