using System;
using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class ParseResult : IParseResult
    {
        public ParseResult(IEnumerable<object> parsedInstances, IEnumerable<ParseException> parseExceptions)
        {
            ParsedInstances = parsedInstances.PreventNull().ToList();
            ParseExceptions = parseExceptions.PreventNull().ToList();
        }

        public void When<T>(Action<T> handler)
        {
            foreach (var instance in ParsedInstances.OfType<T>()) handler(instance);
        }

        public void WhenError(Action<IEnumerable<ParseException>> handler)
        {
            if (ParseExceptions.Any())
                handler(ParseExceptions);
        }

        protected internal IList<object> ParsedInstances { get; set; }
        protected internal IList<ParseException> ParseExceptions { get; set; }
    }
}