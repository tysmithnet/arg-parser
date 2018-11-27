// ***********************************************************************
// Assembly         : ArgParser.Styles.Extensions
// Author           : @tysmithnet
// Created          : 11-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="ITemplateRenderer.cs" company="ArgParser.Styles.Extensions">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ArgParser.Styles.Extensions
{
    /// <summary>
    ///     Represents something that is capable of rendering a template
    /// </summary>
    public interface ITemplateRenderer
    {
        /// <summary>
        ///     Renders the specified template.
        /// </summary>
        /// <param name="template">The template.</param>
        void Render(ITemplate template);
    }
}