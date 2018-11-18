using System.Collections.Generic;
using System.Text;
using ArgParser.Core;
using HelpRequestCallback = System.Func<System.Collections.Generic.Dictionary<object, ArgParser.Core.Parser>, System.Collections.Generic.IEnumerable<ArgParser.Core.ParseException>, string>;
namespace ArgParser.Styles.Alba
{
    public static class ContextBuilderExtensions
    {
        internal static readonly Dictionary<Parser, Theme> ParserThemes = new Dictionary<Parser, Theme>();

        public static ParserBuilder WithTheme(this ParserBuilder builder, Theme theme)
        {
            if(!ParserThemes.ContainsKey(builder.Parser))
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

        public static ContextBuilder AddAutoHelp(this ContextBuilder builder, HelpRequestCallback callback)
        {
            builder.ParseStrategyCreated += (sender, args) =>
            {
                args.ParseStrategy.ParseResultFactory =
                    new HelpRequestedParseResultFactory(args.ParseStrategy.ParseResultFactory, callback, builder.Context);
            };
            return builder;
        }
    }
}
