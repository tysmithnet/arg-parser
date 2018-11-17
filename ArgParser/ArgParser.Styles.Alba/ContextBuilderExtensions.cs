using System;
using System.Collections.Generic;
using System.Text;
using ArgParser.Core;

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

        public static ParserBuilder WithAutoHelp(this ParserBuilder builder, Func<object, Parser, bool> isHelpRequested)
        {
            return builder;
        }
    }
}
