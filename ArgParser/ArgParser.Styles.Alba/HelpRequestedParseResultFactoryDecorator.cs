using System;
using System.Collections.Generic;
using ArgParser.Core;
using ArgParser.Styles.ParseStrategy;

namespace ArgParser.Styles.Alba
{
    public class HelpRequestedParseResultFactoryDecorator : IParseResultFactory
    {
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

        public IContext Context { get; set; }

        protected internal Func<Dictionary<object, Parser>, IEnumerable<ParseException>, bool> HelpRequestedCallback
        {
            get;
            set;
        }

        protected internal IParseResultFactory ParseResultFactory { get; set; }
    }
}