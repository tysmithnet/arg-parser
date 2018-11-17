using System.Collections.Generic;
using ArgParser.Core;

namespace ArgParser.Styles
{
    public class ParseResultFactory : IParseResultFactory
    {
        public ParseResultFactory(IContext context)
        {
            Context = context.ThrowIfArgumentNull(nameof(context));
        }

        public IContext Context { get; private set; }

        public IParseResult Create(Dictionary<object, Parser> results, IEnumerable<ParseException> parseExceptions)
        {
            return new ParseResult(results, parseExceptions);
        }
    }
}