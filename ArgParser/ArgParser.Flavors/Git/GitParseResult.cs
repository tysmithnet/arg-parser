using System;
using System.Collections.Generic;
using ArgParser.Core;
using ArgParser.Core.Validation;

namespace ArgParser.Flavors.Git
{
    public class GitParseResult : IParseResult
    {
        public GitParseResult(IEnumerable<object> parsedInstances, Dictionary<object, IEnumerable<ParseException>> instanceErrors, IEnumerable<ParseException> globalErrors)
        {
            ParsedInstances = parsedInstances.PreventNull();
            InstanceErrors = instanceErrors ?? new Dictionary<object, IEnumerable<ParseException>>();
            GlobalErrors = globalErrors.PreventNull();
        }

        public IEnumerable<ParseException> GlobalErrors { get; set; }
        public Dictionary<object, IEnumerable<ParseException>> InstanceErrors { get; set; }
        public IEnumerable<object> ParsedInstances { get; set; }

        /// <inheritdoc />
        public IParseResult When<T>(Action<T> callback)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IParseResult OnError(Action<IEnumerable<ParseException>> callback)
        {
            throw new NotImplementedException();
        }
    }
}