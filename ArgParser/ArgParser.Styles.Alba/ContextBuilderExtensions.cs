// ***********************************************************************
// Assembly         : ArgParser.Styles.Alba
// Author           : @tysmithnet
// Created          : 11-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="ContextBuilderExtensions.cs" company="ArgParser.Styles.Alba">
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

namespace ArgParser.Styles.Alba
{
    /// <summary>
    ///     Convenience extensions for ContextBuilder
    /// </summary>
    public static class ContextBuilderExtensions
    {
        /// <summary>
        ///     The alba contexts
        /// </summary>
        internal static readonly Dictionary<IContext, AlbaContext> AlbaContexts =
            new Dictionary<IContext, AlbaContext>();

        /// <summary>
        ///     The parser themes
        /// </summary>
        internal static readonly Dictionary<Parser, Theme> ParserThemes = new Dictionary<Parser, Theme>();

        /// <summary>
        ///     Adds automatic help.
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
                var albaContext = builder.Context.ToAlbaContext();
                ParserThemes.ToList().ForEach(kvp => albaContext.ThemeRepository.SetTheme(kvp.Key.Id, kvp.Value));
                args.ParseStrategy.Context = albaContext;
                var factory = new HelpRequestedParseResultFactory(args.ParseStrategy.ParseResultFactory, callback,
                    builder.Context);
                if (setupCallback != null)
                    factory = setupCallback(factory);
                args.ParseStrategy.ParseResultFactory =
                    factory;
            };
            return builder;
        }

        /// <summary>
        ///     Registers alba.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>ContextBuilder.</returns>
        public static ContextBuilder RegisterAlba(this ContextBuilder builder)
        {
            AlbaContexts.Add(builder.Context, new AlbaContext(builder.Context));
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
            ParserThemes[parser] = theme;
            return builder;
        }

        /// <summary>
        ///     Create or get the AlbaContext associated with the provided context
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>AlbaContext.</returns>
        public static AlbaContext ToAlbaContext(this IContext context)
        {
            if(!AlbaContexts.ContainsKey(context))
                AlbaContexts[context] = new AlbaContext(context);
            return AlbaContexts[context];
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