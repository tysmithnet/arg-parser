// ***********************************************************************
// Assembly         : ArgParser.Styles.Alba
// Author           : dts50
// Created          : 11-22-2018
//
// Last Modified By : dts50
// Last Modified On : 11-22-2018
// ***********************************************************************
// <copyright file="BannerArtDiv.cs" company="tysmith.net">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using Alba.CsConsoleFormat;
using ArgParser.Core;
using Figgle;

namespace ArgParser.Styles.Alba
{
    /// <summary>
    ///     Element that renders an ASCII art banner
    ///     Implements the <see cref="Alba.CsConsoleFormat.BlockElement" />
    /// </summary>
    /// <seealso cref="Alba.CsConsoleFormat.BlockElement" />
    public class BannerArtDiv : BlockElement
    {
        /// <summary>
        ///     Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }

        /// <summary>
        ///     Gets or sets the font.
        /// </summary>
        /// <value>The font.</value>
        public FiggleFont Font { get; set; } = FiggleFonts.Basic;

        /// <summary>
        ///     Generates the visual elements.
        /// </summary>
        /// <returns>IEnumerable&lt;Element&gt;.</returns>
        public override IEnumerable<Element> GenerateVisualElements()
        {
            var text = Font.Render(Text);
            return new Span(text)
            {
                Color = Color
            }.ToEnumerableOfOne();
        }
    }
}