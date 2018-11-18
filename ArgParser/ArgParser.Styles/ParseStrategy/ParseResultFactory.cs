using System.Collections.Generic;
using ArgParser.Core;

namespace ArgParser.Styles.ParseStrategy
{
    public class ParseResultFactory : IParseResultFactory
    {
        public ParseResultFactory(IContext context)
        {
            Context = context.ThrowIfArgumentNull(nameof(context));
        }

        public IParseResult Create(Dictionary<object, Parser> results, IEnumerable<ParseException> parseExceptions) =>
            new ParseResult(results, parseExceptions);

        public IContext Context { get; set; }
    }
}