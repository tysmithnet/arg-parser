// ***********************************************************************
// Assembly         : ArgParser.Styles.Alba
// Author           : @tysmithnet
// Created          : 11-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="Theme.cs" company="ArgParser.Styles.Alba">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Diagnostics.CodeAnalysis;
using Figgle;

namespace ArgParser.Styles.Alba
{
    /// <summary>
    ///     Represents a cohesive set of colors that will be used when rendering text to the console
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class Theme
    {
        /// <summary>
        ///     The basic
        /// </summary>
        public static readonly Theme Basic = new BasicTheme();

        /// <summary>
        ///     The cool
        /// </summary>
        public static readonly Theme Cool = new CoolTheme();

        /// <summary>
        ///     The default
        /// </summary>
        public static readonly Theme Default = new DefaultTheme();

        /// <summary>
        ///     The warm
        /// </summary>
        public static readonly Theme Warm = new WarmTheme();

        /// <summary>
        ///     Creates the specified default text.
        /// </summary>
        /// <param name="defaultText">The default text.</param>
        /// <param name="secondaryText">The secondary text.</param>
        /// <param name="codeText">The code text.</param>
        /// <param name="requiredColor">Color of the required.</param>
        /// <param name="borderColor">Color of the border.</param>
        /// <returns>Theme.</returns>
        public static Theme Create(ConsoleColor defaultText = ConsoleColor.White,
            ConsoleColor secondaryText = ConsoleColor.White, ConsoleColor codeText = ConsoleColor.White,
            ConsoleColor requiredColor = ConsoleColor.White, ConsoleColor borderColor = ConsoleColor.White) =>
            new DefaultTheme
            {
                DefaultTextColor = defaultText,
                SecondaryTextColor = secondaryText,
                CodeColor = codeText,
                RequiredColor = requiredColor,
                BorderColor = borderColor
            };

        /// <summary>
        ///     Gets or sets the banner font.
        /// </summary>
        /// <value>The banner font.</value>
        public virtual FiggleFont BannerFont { get; protected internal set; } = FiggleFonts.Poison;

        /// <summary>
        ///     Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        public virtual ConsoleColor BorderColor { get; protected internal set; } = ConsoleColor.White;

        /// <summary>
        ///     Gets or sets the color of the code.
        /// </summary>
        /// <value>The color of the code.</value>
        public virtual ConsoleColor CodeColor { get; protected internal set; } = ConsoleColor.White;

        /// <summary>
        ///     Gets or sets the default color of the text.
        /// </summary>
        /// <value>The default color of the text.</value>
        public virtual ConsoleColor DefaultTextColor { get; protected internal set; } = ConsoleColor.White;

        /// <summary>
        ///     Gets or sets the color of the required.
        /// </summary>
        /// <value>The color of the required.</value>
        public virtual ConsoleColor RequiredColor { get; protected internal set; } = ConsoleColor.White;

        /// <summary>
        ///     Gets or sets the color of the secondary text.
        /// </summary>
        /// <value>The color of the secondary text.</value>
        public virtual ConsoleColor SecondaryTextColor { get; protected internal set; } = ConsoleColor.White;

        /// <summary>
        ///     Class BasicTheme.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Theme" />
        private class BasicTheme : Theme
        {
            /// <summary>
            ///     Gets or sets the color of the border.
            /// </summary>
            /// <value>The color of the border.</value>
            public override ConsoleColor BorderColor { get; protected internal set; } = ConsoleColor.Magenta;

            /// <summary>
            ///     Gets or sets the color of the code.
            /// </summary>
            /// <value>The color of the code.</value>
            public override ConsoleColor CodeColor { get; protected internal set; } = ConsoleColor.Yellow;

            /// <summary>
            ///     Gets or sets the default color of the text.
            /// </summary>
            /// <value>The default color of the text.</value>
            public override ConsoleColor DefaultTextColor { get; protected internal set; } = ConsoleColor.Green;

            /// <summary>
            ///     Gets or sets the color of the required.
            /// </summary>
            /// <value>The color of the required.</value>
            public override ConsoleColor RequiredColor { get; protected internal set; } = ConsoleColor.Red;

            /// <summary>
            ///     Gets or sets the color of the secondary text.
            /// </summary>
            /// <value>The color of the secondary text.</value>
            public override ConsoleColor SecondaryTextColor { get; protected internal set; } = ConsoleColor.Blue;
        }

        /// <summary>
        ///     Class CoolTheme.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Theme" />
        private class CoolTheme : Theme
        {
            /// <summary>
            ///     Gets or sets the color of the code.
            /// </summary>
            /// <value>The color of the code.</value>
            public override ConsoleColor CodeColor { get; protected internal set; } = ConsoleColor.DarkCyan;

            /// <summary>
            ///     Gets or sets the default color of the text.
            /// </summary>
            /// <value>The default color of the text.</value>
            public override ConsoleColor DefaultTextColor { get; protected internal set; } = ConsoleColor.DarkBlue;

            /// <summary>
            ///     Gets or sets the color of the required.
            /// </summary>
            /// <value>The color of the required.</value>
            public override ConsoleColor RequiredColor { get; protected internal set; } = ConsoleColor.Cyan;

            /// <summary>
            ///     Gets or sets the color of the secondary text.
            /// </summary>
            /// <value>The color of the secondary text.</value>
            public override ConsoleColor SecondaryTextColor { get; protected internal set; } = ConsoleColor.Cyan;
        }

        /// <summary>
        ///     Class DefaultTheme.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Theme" />
        private class DefaultTheme : Theme
        {
            /// <summary>
            ///     Gets or sets the color of the border.
            /// </summary>
            /// <value>The color of the border.</value>
            public override ConsoleColor BorderColor { get; protected internal set; } = ConsoleColor.Yellow;

            /// <summary>
            ///     Gets or sets the color of the code.
            /// </summary>
            /// <value>The color of the code.</value>
            public override ConsoleColor CodeColor { get; protected internal set; } = ConsoleColor.Green;

            /// <summary>
            ///     Gets or sets the color of the required.
            /// </summary>
            /// <value>The color of the required.</value>
            public override ConsoleColor RequiredColor { get; protected internal set; } = ConsoleColor.Red;

            /// <summary>
            ///     Gets or sets the color of the secondary text.
            /// </summary>
            /// <value>The color of the secondary text.</value>
            public override ConsoleColor SecondaryTextColor { get; protected internal set; } = ConsoleColor.Yellow;
        }

        /// <summary>
        ///     Class WarmTheme.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Theme" />
        private class WarmTheme : Theme
        {
            /// <summary>
            ///     Gets or sets the color of the border.
            /// </summary>
            /// <value>The color of the border.</value>
            public override ConsoleColor BorderColor { get; protected internal set; } = ConsoleColor.DarkYellow;

            /// <summary>
            ///     Gets or sets the color of the code.
            /// </summary>
            /// <value>The color of the code.</value>
            public override ConsoleColor CodeColor { get; protected internal set; } = ConsoleColor.Red;

            /// <summary>
            ///     Gets or sets the default color of the text.
            /// </summary>
            /// <value>The default color of the text.</value>
            public override ConsoleColor DefaultTextColor { get; protected internal set; } = ConsoleColor.Yellow;

            /// <summary>
            ///     Gets or sets the color of the secondary text.
            /// </summary>
            /// <value>The color of the secondary text.</value>
            public override ConsoleColor SecondaryTextColor { get; protected internal set; } = ConsoleColor.DarkRed;
        }
    }
}