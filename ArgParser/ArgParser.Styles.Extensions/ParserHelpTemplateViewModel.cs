// ***********************************************************************
// Assembly         : ArgParser.Styles.Extensions
// Author           : @tysmithnet
// Created          : 11-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-19-2018
// ***********************************************************************
// <copyright file="ParserHelpTemplateViewModel.cs" company="ArgParser.Styles.Extensions">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alba.CsConsoleFormat;
using ArgParser.Core;

namespace ArgParser.Styles.Extensions
{
    /// <summary>
    ///     View model for the default help template
    /// </summary>
    public class ParserHelpTemplateViewModel
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ParserHelpTemplateViewModel" /> class.
        /// </summary>
        /// <param name="chain">The chain.</param>
        /// <param name="parameterVms">The parameter VMS.</param>
        /// <param name="subCommands">The sub commands.</param>
        /// <exception cref="ArgumentNullException">
        ///     chain
        ///     or
        ///     parameterVms
        ///     or
        ///     subCommands
        /// </exception>
        public ParserHelpTemplateViewModel(IList<ParserViewModel> chain, IList<ParameterViewModel> parameterVms,
            IList<ParserViewModel> subCommands)
        {
            Chain = chain ?? throw new ArgumentNullException(nameof(chain));
            ParameterVms = parameterVms ?? throw new ArgumentNullException(nameof(parameterVms));
            SubCommands = subCommands ?? throw new ArgumentNullException(nameof(subCommands));
        }

        /// <summary>
        ///     Gets or sets the chain.
        /// </summary>
        /// <value>The chain.</value>
        public IList<ParserViewModel> Chain { get; protected internal set; }

        /// <summary>
        ///     Gets the line thickness.
        /// </summary>
        /// <value>The line thickness.</value>
        public LineThickness LineThickness => new LineThickness(LineWidth.Single, LineWidth.Single);

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
        ///     Gets or sets the sub commands.
        /// </summary>
        /// <value>The sub commands.</value>
        public IList<ParserViewModel> SubCommands { get; protected internal set; }

        /// <summary>
        ///     Gets the sub title.
        /// </summary>
        /// <value>The sub title.</value>
        public virtual string SubTitle
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