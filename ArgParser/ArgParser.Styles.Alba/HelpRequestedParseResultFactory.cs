using System;
using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public class HelpRequestedParseResultFactory : IParseResultFactory
    {
        public HelpRequestedParseResultFactory(IParseResultFactory inner,
            Func<Dictionary<object, Parser>, IEnumerable<ParseException>, string> helpRequestedCallback,
            IContext context)
        {
            Inner = inner.ThrowIfArgumentNull(nameof(inner));
            IsHelpRequestedCallback = helpRequestedCallback.ThrowIfArgumentNull(nameof(helpRequestedCallback));
            Context = context.ThrowIfArgumentNull(nameof(context));
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
        public ITemplateRenderer TemplateRenderer { get; set; } = new TemplateRenderer();

        protected internal Func<Dictionary<object, Parser>, IEnumerable<ParseException>, string> IsHelpRequestedCallback
        {
            get;
            set;
        }
    }
}