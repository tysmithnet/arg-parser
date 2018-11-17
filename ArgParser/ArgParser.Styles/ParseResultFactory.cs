using System.Collections.Generic;
using ArgParser.Core;

namespace ArgParser.Styles
{
    public class ParseResultFactory : IParseResultFactory
    {
        public IParseResult Create(Dictionary<object, Parser> results, IEnumerable<ParseException> parseExceptions)
        {
            return new ParseResult(results, parseExceptions);
        }
    }
}