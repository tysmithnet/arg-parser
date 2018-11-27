// ***********************************************************************
// Assembly         : ArgParser.Styles.Extensions
// Author           : @tysmithnet
// Created          : 11-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-25-2018
// ***********************************************************************
// <copyright file="ThemeRepository.cs" company="ArgParser.Styles.Extensions">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace ArgParser.Styles.Extensions
{
    /// <summary>
    ///     Default IThemeRepository
    /// </summary>
    /// <seealso cref="ArgParser.Styles.Extensions.IThemeRepository" />
    public class ThemeRepository : IThemeRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ThemeRepository" /> class.
        /// </summary>
        /// <param name="themes">The themes.</param>
        public ThemeRepository(Dictionary<string, Theme> themes = null)
        {
            if (themes == null) return;
            Themes = themes;
        }

        /// <summary>
        ///     Gets the specified parser identifier.
        /// </summary>
        /// <param name="parserId">The parser identifier.</param>
        /// <returns>Theme.</returns>
        public virtual Theme Get(string parserId) => !Themes.ContainsKey(parserId) ? Default : Themes[parserId];

        /// <summary>
        ///     Sets the theme.
        /// </summary>
        /// <param name="parserId">The parser identifier.</param>
        /// <param name="theme">The theme.</param>
        public virtual void SetTheme(string parserId, Theme theme)
        {
            if (!Themes.ContainsKey(parserId))
                Themes.Add(parserId, theme);
            Themes[parserId] = theme;
        }

        /// <summary>
        ///     Gets the default.
        /// </summary>
        /// <value>The default.</value>
        public Theme Default { get; } = Theme.Default;

        /// <summary>
        ///     Gets or sets the themes.
        /// </summary>
        /// <value>The themes.</value>
        protected internal Dictionary<string, Theme> Themes { get; set; } = new Dictionary<string, Theme>();
    }
}