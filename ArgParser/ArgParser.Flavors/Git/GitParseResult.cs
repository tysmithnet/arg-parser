using System;
using System.Collections.Generic;
using ArgParser.Core;
using ArgParser.Core.Validation;

namespace ArgParser.Flavors.Git
{
    public class GitParseResult : IParseResult
    {
        public GitParseResult(IEnumerable<object> parsedInstances, Dictionary<object, IEnumerable<ParseError>> instanceErrors, IEnumerable<ParseError> globalErrors)
        {
            ParsedInstances = parsedInstances.PreventNull();
            InstanceErrors = instanceErrors ?? new Dictionary<object, IEnumerable<ParseError>>();
            GlobalErrors = globalErrors.PreventNull();
        }

        public IEnumerable<ParseError> GlobalErrors { get; set; }
        public Dictionary<object, IEnumerable<ParseError>> InstanceErrors { get; set; }
        public IEnumerable<object> ParsedInstances { get; set; }

        /// <inheritdoc />
        public IParseResult When<T>(Action<T> callback)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IParseResult OnError(Action<IEnumerable<ParseError>> callback)
        {
            throw new NotImplementedException();
        }
    }
}