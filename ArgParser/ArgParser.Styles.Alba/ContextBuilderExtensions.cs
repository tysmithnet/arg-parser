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
            var decorator = new HelpRequestedParseResultFactoryDecorator(builder.);
            return builder;
        }
    }

    public class HelpRequestedParseResultFactoryDecorator : IParseResultFactory
    {
        protected internal IParseResultFactory ParseResultFactory { get; set; }
        protected internal Func<Dictionary<object, Parser>, IEnumerable<ParseException>, bool> HelpRequestedCallback { get; set; }

        public HelpRequestedParseResultFactoryDecorator(IParseResultFactory parseResultFactory,
            Func<Dictionary<object, Parser>, IEnumerable<ParseException>, bool> helpRequestedCallback)
        {
            ParseResultFactory = parseResultFactory ?? throw new ArgumentNullException(nameof(parseResultFactory));
            HelpRequestedCallback =
                helpRequestedCallback ?? throw new ArgumentNullException(nameof(helpRequestedCallback));
        }

        public HelpRequestedParseResultFactoryDecorator(IParseResultFactory parseResultFactory)
        {
            ParseResultFactory = parseResultFactory ?? throw new ArgumentNullException(nameof(parseResultFactory));
        }

        public IParseResult Create(Dictionary<object, Parser> results, IEnumerable<ParseException> parseExceptions)
        {

            var res = ParseResultFactory.Create(results, parseExceptions);
            return res;
        }
    }
}
