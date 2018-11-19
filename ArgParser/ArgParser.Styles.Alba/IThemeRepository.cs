// ***********************************************************************
// Assembly         : ArgParser.Styles.Alba
// Author           : @tysmithnet
// Created          : 11-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="IThemeRepository.cs" company="ArgParser.Styles.Alba">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ArgParser.Styles.Alba
{
    /// <summary>
    ///     Represents something that is capable of managing the themes for the parsers
    /// </summary>
    public interface IThemeRepository
    {
        /// <summary>
        ///     Gets the specified parser identifier.
        /// </summary>
        /// <param name="parserId">The parser identifier.</param>
        /// <returns>Theme.</returns>
        Theme Get(string parserId);

        /// <summary>
        ///     Sets the theme.
        /// </summary>
        /// <param name="parserId">The parser identifier.</param>
        /// <param name="theme">The theme.</param>
        void SetTheme(string parserId, Theme theme);

        /// <summary>
        ///     Gets the default.
        /// </summary>
        /// <value>The default.</value>
        Theme Default { get; }
    }
}