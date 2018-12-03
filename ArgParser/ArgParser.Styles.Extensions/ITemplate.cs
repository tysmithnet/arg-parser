// ***********************************************************************
// Assembly         : ArgParser.Styles.Extensions
// Author           : @tysmithnet
// Created          : 11-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="ITemplate.cs" company="ArgParser.Styles.Extensions">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Alba.CsConsoleFormat;

namespace ArgParser.Styles.Extensions
{
    /// <summary>
    ///     Represents an object that is capapble of turning a view model into a document for rendering
    /// </summary>
    public interface ITemplate
    {
        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <returns>Document.</returns>
        Document Create();
    }
}