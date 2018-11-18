using System.Collections.Generic;
using ArgParser.Core;
using HelpRequestCallback =
    System.Func<System.Collections.Generic.Dictionary<object, ArgParser.Core.Parser>,
        System.Collections.Generic.IEnumerable<ArgParser.Core.ParseException>, string>;

namespace ArgParser.Styles.Alba
{
    public static class ContextBuilderExtensions
    {
        internal static readonly Dictionary<IContext, AlbaContext> AlbaContexts =
            new Dictionary<IContext, AlbaContext>();

        internal static readonly Dictionary<Parser, Theme> ParserThemes = new Dictionary<Parser, Theme>();

        public static ContextBuilder AddAutoHelp(this ContextBuilder builder, HelpRequestCallback callback)
        {
            builder.ParseStrategyCreated += (sender, args) =>
            {
                var albaContext = builder.Context.ToAlbaContext();
                albaContext.Themes = ParserThemes;
                args.ParseStrategy.Context = albaContext;
                args.ParseStrategy.ParseResultFactory =
                    new HelpRequestedParseResultFactory(args.ParseStrategy.ParseResultFactory, callback,
                        builder.Context);
            };
            return builder;
        }

        public static ContextBuilder RegisterAlba(this ContextBuilder builder)
        {
            AlbaContexts.Add(builder.Context, new AlbaContext(builder.Context));
            return builder;
        }

        public static ContextBuilder SetTheme(this ContextBuilder builder, string parserId, Theme theme)
        {
            var parser = builder.Context.ParserRepository.Get(parserId);
            if (!ParserThemes.ContainsKey(parser))
                ParserThemes.Add(parser, theme);
            ParserThemes[parser] = theme;
            return builder;
        }

        public static AlbaContext ToAlbaContext(this IContext context) => AlbaContexts[context];

        public static ParserBuilder WithTheme(this ParserBuilder builder, Theme theme)
        {
            if (!ParserThemes.ContainsKey(builder.Parser))
                ParserThemes.Add(builder.Parser, theme);
            ParserThemes[builder.Parser] = theme;
            return builder;
        }

        public static ParserBuilder<T> WithTheme<T>(this ParserBuilder<T> builder, Theme theme)
        {
            if (!ParserThemes.ContainsKey(builder.Parser))
                ParserThemes.Add(builder.Parser, theme);
            ParserThemes[builder.Parser] = theme;
            return builder;
        }
    }
}