// ***********************************************************************
// Assembly         : ArgParser.Styles.Extensions
// Author           : @tysmithnet
// Created          : 11-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="ContextBuilderExtensions.cs" company="ArgParser.Styles.Extensions">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;
using HelpRequestCallback =
    System.Func<System.Collections.Generic.Dictionary<object, ArgParser.Core.Parser>,
        System.Collections.Generic.IEnumerable<ArgParser.Core.ParseException>, string>;

namespace ArgParser.Styles.Extensions
{
    /// <summary>
    ///     Convenience extensions for ContextBuilder
    /// </summary>
    public static class ContextBuilderExtensions
    {
        /// <summary>
        ///     The extension contexts
        /// </summary>
        internal static readonly Dictionary<IContext, ExtensionContext> ExtensionContexts =
            new Dictionary<IContext, ExtensionContext>();

        /// <summary>
        ///     The parser themes
        /// </summary>
        internal static readonly Dictionary<Parser, Theme> ParserThemes = new Dictionary<Parser, Theme>();

        /// <summary>
        ///     Adds automatic help generation based on the result of the provided callback.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="callback">The callback.</param>
        /// <param name="setupCallback">The setup callback.</param>
        /// <returns>ContextBuilder.</returns>
        public static ContextBuilder AddAutoHelp(this ContextBuilder builder, HelpRequestCallback callback,
            Func<HelpRequestedParseResultFactory, HelpRequestedParseResultFactory> setupCallback = null)
        {
            builder.ParseStrategyCreated += (sender, args) =>
            {
                var extensionContext = builder.Context.ToExtensionContext();
                ParserThemes.ToList().ForEach(kvp => extensionContext.ThemeRepository.SetTheme(kvp.Key.Id, kvp.Value));
                args.ParseStrategy.Context = extensionContext;
                var factory = new HelpRequestedParseResultFactory(args.ParseStrategy.ParseResultFactory, callback,
                    builder.Context);
                if (setupCallback != null)
                    factory = setupCallback(factory);
                args.ParseStrategy.ParseResultFactory =
                    factory;
            };
            return builder;
        }

        public static ContextBuilder SetSwitchTokens(this ContextBuilder builder, string letterToken, string wordToken)
        {
            if(letterToken.IsNullOrWhiteSpace())
                throw new ArgumentException($"Expected a valid letter token, but received: {letterToken}");
            if (wordToken.IsNullOrWhiteSpace())
                throw new ArgumentException($"Expected a valid word token, but received: {wordToken}");
            foreach (var parser in builder.Context.ParserRepository.GetAll())
            {
                foreach (var param in parser.Parameters.OfType<Switch>())
                {
                    param.LetterToken = letterToken;
                    param.WordToken = wordToken;
                }
            }
            builder.ParameterCreated += (sender, args) =>
            {
                if (!(args.Parameter is Switch casted)) return;
                casted.LetterToken = letterToken;
                casted.WordToken = wordToken;
            };

            return builder;
        }
        
        /// <summary>
        ///     Registers extension modules.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>ContextBuilder.</returns>
        public static ContextBuilder RegisterExtensions(this ContextBuilder builder)
        {
            ExtensionContexts.Add(builder.Context, new ExtensionContext(builder.Context));
            return builder;
        }

        /// <summary>
        ///     Sets the theme.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="parserId">The parser identifier.</param>
        /// <param name="theme">The theme.</param>
        /// <returns>ContextBuilder.</returns>
        public static ContextBuilder SetTheme(this ContextBuilder builder, string parserId, Theme theme)
        {
            var parser = builder.Context.ParserRepository.Get(parserId);
            if (!ParserThemes.ContainsKey(parser))
                ParserThemes.Add(parser, theme);
            else
                ParserThemes[parser] = theme;
            return builder;
        }

        /// <summary>
        ///     Create or get the extension context associated with the provided context
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The extension context associated with the provided context.</returns>
        public static ExtensionContext ToExtensionContext(this IContext context)
        {
            var allParsers = context.ParserRepository.GetAll().ToList();
            var allParserThemes = ParserThemes.Where(t => allParsers.Contains(t.Key));
            if (!ExtensionContexts.ContainsKey(context))
                ExtensionContexts[context] = new ExtensionContext(context)
                {
                    ThemeRepository = new ThemeRepository()
                    {
                        Themes = allParserThemes.ToDictionary(x => x.Key.Id, x => x.Value)
                    }
                };
            return ExtensionContexts[context];
        }

        /// <summary>
        ///     Sets the theme.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="theme">The theme.</param>
        /// <returns>ParserBuilder.</returns>
        public static ParserBuilder WithTheme(this ParserBuilder builder, Theme theme)
        {
            if (!ParserThemes.ContainsKey(builder.Parser))
                ParserThemes.Add(builder.Parser, theme);
            else
                ParserThemes[builder.Parser] = theme;
            return builder;
        }

        /// <summary>
        ///     Sets the theme.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="theme">The theme.</param>
        /// <returns>ParserBuilder&lt;T&gt;.</returns>
        public static ParserBuilder<T> WithTheme<T>(this ParserBuilder<T> builder, Theme theme)
        {
            if (!ParserThemes.ContainsKey(builder.Parser))
                ParserThemes.Add(builder.Parser, theme);
            ParserThemes[builder.Parser] = theme;
            return builder;
        }
    }
}