using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public class DefaultParseResult : IParseResult
    {
        public DefaultParseResult(IList<object> parsedInstances, IEnumerable<ParseError> errors = null)
        {
            ParsedInstances = parsedInstances.ThrowIfArgumentNull(nameof(parsedInstances));
            ParseErrors = errors?.ToList();
        }

        public IList<ParseError> ParseErrors { get; set; }

        public IParseResult When<T>(Action<T> callback)
        {
            foreach (var parsedInstance in ParsedInstances.OfType<T>()) callback(parsedInstance);
            return this;
        }

        public IParseResult OnError(Action<IEnumerable<ParseError>> callback)
        {
            if (ParseErrors != null)
            {
                callback(ParseErrors.ToList());
            }

            return this;
        }

        public IList<object> ParsedInstances { get; set; }
    }
}