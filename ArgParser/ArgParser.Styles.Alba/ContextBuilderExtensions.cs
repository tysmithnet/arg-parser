using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alba.CsConsoleFormat;
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


    public class HelpRequestedParseResultFactory : IParseResultFactory
    {
        public IParseResultFactory Inner { get; set; }
        protected internal Func<Dictionary<object, Parser>, IEnumerable<ParseException>, string> IsHelpRequestedCallback { get; set; }
        protected internal IContext Context { get; set; }

        public HelpRequestedParseResultFactory(IParseResultFactory inner,
            HelpRequestCallback helpRequestedCallback,
            IContext context)
        {
            Inner = inner ?? throw new ArgumentNullException(nameof(inner));
            IsHelpRequestedCallback = helpRequestedCallback ??
                                      throw new ArgumentNullException(nameof(helpRequestedCallback));
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IParseResult Create(Dictionary<object, Parser> results, IEnumerable<ParseException> parseExceptions)
        {
            parseExceptions = parseExceptions.PreventNull().ToList();
            var helpRequest = IsHelpRequestedCallback(results, parseExceptions);
            if (helpRequest.IsNotNullOrWhiteSpace())
            {
                var writer = new ParserHelpTemplate(Context, helpRequest);
                var doc = writer.Create();
                ConsoleRenderer.RenderDocument(doc);
                return new ParseResult(null, null);
            }
            return Inner.Create(results, parseExceptions);
        }
    }
}
