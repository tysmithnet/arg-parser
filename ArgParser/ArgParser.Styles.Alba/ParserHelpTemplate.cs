﻿// ***********************************************************************
// Assembly         : ArgParser.Styles.Alba
// Author           : @tysmithnet
// Created          : 11-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="ParserHelpTemplate.cs" company="ArgParser.Styles.Alba">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.IO;
using System.Linq;
using Alba.CsConsoleFormat;
using Alba.CsConsoleFormat.ColorfulConsole;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    /// <summary>
    ///     Default help template for Parsers
    /// </summary>
    /// <seealso cref="ArgParser.Styles.Alba.ITemplate" />
    public class ParserHelpTemplate : ITemplate
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ParserHelpTemplate" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="parserId">The parser identifier.</param>
        public ParserHelpTemplate(IContext context, string parserId)
        {
            Context = context.ToAlbaContext();
            Parser = context.ParserRepository.Get(parserId);
            ViewModel = CreateViewModel(context);
        }

        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <returns>Document.</returns>
        public Document Create()
        {
            using (var fs = File.OpenRead("ParserHelp.xaml"))
            {
                return ConsoleRenderer.ReadDocumentFromStream(fs, ViewModel, new XamlElementReaderSettings
                {
                    ReferenceAssemblies =
                    {
                        typeof(FigletDiv).Assembly,
                        typeof(ParserHelpTemplate).Assembly,
                        typeof(Parser).Assembly
                    }
                });
            }
        }

        /// <summary>
        ///     Creates the view model.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>ParserHelpTemplateViewModel.</returns>
        protected internal ParserHelpTemplateViewModel CreateViewModel(IContext context)
        {
            var albaContext = context.ToAlbaContext();
            var vm = new ParserHelpTemplateViewModel
            {
                Chain = albaContext.PathToRoot(Parser.Id).Reverse().Select(x =>
                    new ParserViewModel(x, albaContext.ThemeRepository.Get(x.Id))
                    {
                        Alias = context.AliasRepository.HasAlias(x.Id) ? context.AliasRepository.GetAlias(x.Id) : null
                    }).ToList()
            };
            vm.ParameterVms = vm.Chain
                .SelectMany(x => x.Parser.Parameters.Select(y => new ParameterViewModel(y, x.Theme))).ToList();
            return vm;
        }

        /// <summary>
        ///     Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        protected internal AlbaContext Context { get; set; }

        /// <summary>
        ///     Gets or sets the parser.
        /// </summary>
        /// <value>The parser.</value>
        protected internal Parser Parser { get; set; }

        /// <summary>
        ///     Gets or sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        protected internal ParserHelpTemplateViewModel ViewModel { get; set; }
    }
}