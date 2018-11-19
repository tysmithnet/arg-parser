// ***********************************************************************
// Assembly         : ArgParser.Styles.Alba
// Author           : @tysmithnet
// Created          : 11-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-19-2018
// ***********************************************************************
// <copyright file="ParserHelpTemplateViewModel.cs" company="ArgParser.Styles.Alba">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    /// <summary>
    ///     View model for the default help template
    /// </summary>
    public class ParserHelpTemplateViewModel
    {
        /// <summary>
        ///     Gets the color of the banner.
        /// </summary>
        /// <value>The color of the banner.</value>
        public string BannerColor =>
            $"{ParserVm.Theme.DefaultTextColor} 0; {ParserVm.Theme.SecondaryTextColor}; {ParserVm.Theme.CodeColor} 3";

        /// <summary>
        ///     Gets or sets the chain.
        /// </summary>
        /// <value>The chain.</value>
        public IList<ParserViewModel> Chain { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the parameter VMS.
        /// </summary>
        /// <value>The parameter VMS.</value>
        public IList<ParameterViewModel> ParameterVms { get; protected internal set; }

        /// <summary>
        ///     Gets the parser vm.
        /// </summary>
        /// <value>The parser vm.</value>
        public ParserViewModel ParserVm => Chain.Last();

        /// <summary>
        ///     Gets the sub title.
        /// </summary>
        /// <value>The sub title.</value>
        public string SubTitle
        {
            get
            {
                var sb = new StringBuilder(ParserVm.Parser.Id);
                if (ParserVm.Parser.Help.Version.IsNotNullOrWhiteSpace())
                    sb.Append($" - {ParserVm.Parser.Help.Version}");
                if (ParserVm.Parser.Help.ShortDescription.IsNotNullOrWhiteSpace())
                    sb.Append($" - {ParserVm.Parser.Help.ShortDescription}");
                return sb.ToString();
            }
        }
    }
}