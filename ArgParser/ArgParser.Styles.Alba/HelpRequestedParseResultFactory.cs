using System;
using System.Collections.Generic;
using System.Linq;
using Alba.CsConsoleFormat;
using ArgParser.Core;
using ArgParser.Styles.ParseStrategy;

namespace ArgParser.Styles.Alba
{
    public class HelpRequestedParseResultFactory : IParseResultFactory
    {
        public ITemplateRenderer TemplateRenderer { get; set; } = new TemplateRenderer();

        public HelpRequestedParseResultFactory(IParseResultFactory inner,
            Func<Dictionary<object, Parser>, IEnumerable<ParseException>, string> helpRequestedCallback,
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
                var template = new ParserHelpTemplate(Context, helpRequest);
                TemplateRenderer.Render(template);
                return new ParseResult(null, null);
            }

            return Inner.Create(results, parseExceptions);
        }

        public IContext Context { get; set; }
        public IParseResultFactory Inner { get; set; }

        protected internal Func<Dictionary<object, Parser>, IEnumerable<ParseException>, string> IsHelpRequestedCallback
        {
            get;
            set;
        }
    }
}