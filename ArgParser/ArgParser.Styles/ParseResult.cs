using System;
using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;
using ArgParser.Core.Extensions;

namespace ArgParser.Styles
{
    public class ParseResult : IParseResult
    {
        public ParseResult(Dictionary<object, Parser> results, IEnumerable<ParseException> parseExceptions)
        {
            Results = results ?? new Dictionary<object, Parser>();
            ParseExceptions = parseExceptions.PreventNull().ToList();
        }

        public void When<T>(Action<T, Parser> handler)
        {
            foreach (var kvp in Results)
                if (kvp.Key is T casted)
                    handler(casted, kvp.Value);
        }

        public void WhenError(Action<IEnumerable<ParseException>> handler)
        {
            if (ParseExceptions.Any())
                handler(ParseExceptions);
        }

        protected internal IList<ParseException> ParseExceptions { get; set; }

        protected internal Dictionary<object, Parser> Results { get; set; }
    }
}