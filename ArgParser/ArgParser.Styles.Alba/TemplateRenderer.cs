// ***********************************************************************
// Assembly         : ArgParser.Styles.Alba
// Author           : @tysmithnet
// Created          : 11-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="TemplateRenderer.cs" company="ArgParser.Styles.Alba">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Diagnostics.CodeAnalysis;
using Alba.CsConsoleFormat;

namespace ArgParser.Styles.Alba
{
    /// <summary>
    ///     Default template renderer
    /// </summary>
    /// <seealso cref="ArgParser.Styles.Alba.ITemplateRenderer" />
    [ExcludeFromCodeCoverage]
    internal class TemplateRenderer : ITemplateRenderer
    {
        /// <summary>
        ///     Renders the specified template.
        /// </summary>
        /// <param name="template">The template.</param>
        public virtual void Render(ITemplate template)
        {
            var doc = template.Create();
            ConsoleRenderer.RenderDocument(doc);
        }
    }
}