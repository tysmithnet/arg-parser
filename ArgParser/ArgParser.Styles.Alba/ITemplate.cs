// ***********************************************************************
// Assembly         : ArgParser.Styles.Alba
// Author           : @tysmithnet
// Created          : 11-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="ITemplate.cs" company="ArgParser.Styles.Alba">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Alba.CsConsoleFormat;

namespace ArgParser.Styles.Alba
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