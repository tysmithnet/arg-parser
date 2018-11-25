// ***********************************************************************
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
        }

        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <returns>Document.</returns>
        public virtual Document Create()
        {
            var vm = CreateViewModel(Context);
            using (var fs = File.OpenRead("ParserHelp.xaml"))
            {
                return ConsoleRenderer.ReadDocumentFromStream(fs, vm, new XamlElementReaderSettings
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
        public virtual ParserHelpTemplateViewModel CreateViewModel(IContext context)
        {
            var albaContext = context.ToAlbaContext();
            var parserViewModels = albaContext.PathToRoot(Parser.Id).Reverse().Select(x =>
                new ParserViewModel(x, albaContext.ThemeRepository.Get(x.Id))
                {
                    Alias = context.AliasRepository.HasAlias(x.Id) ? context.AliasRepository.GetAlias(x.Id) : null
                }).ToList();

            var parameterViewModels = parserViewModels
                .SelectMany(x => x.Parser.Parameters.Select(y => new ParameterViewModel(y, x.Theme))).ToList();

            var subCommandViewModels = albaContext.Context.HierarchyRepository.GetChildren(Parser.Id).Select(x =>
            {
                var parser = albaContext.ParserRepository.Get(x);
                var theme = albaContext.ThemeRepository.Get(x);
                return new ParserViewModel(parser, theme)
                {
                    Alias = albaContext.AliasRepository.HasAlias(parser.Id)
                        ? albaContext.AliasRepository.GetAlias(parser.Id)
                        : parser.Id
                };
            }).ToList();

            return new ParserHelpTemplateViewModel(parserViewModels, parameterViewModels, subCommandViewModels);
        }

        /// <summary>
        ///     Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        public AlbaContext Context { get; set; }

        /// <summary>
        ///     Gets or sets the parser.
        /// </summary>
        /// <value>The parser.</value>
        public Parser Parser { get; set; }

        /// <summary>
        ///     Gets or sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        public ParserHelpTemplateViewModel ViewModel { get; set; }
    }
}