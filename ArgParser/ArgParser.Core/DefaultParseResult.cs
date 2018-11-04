﻿using System;
using System.Collections.Generic;
using System.Linq;
using ArgParser.Core.Validation;

namespace ArgParser.Core
{
    public class DefaultParseResult : IParseResult
    {
        public DefaultParseResult(IList<object> parsedInstances, IEnumerable<ParseException> errors = null)
        {
            ParsedInstances = parsedInstances.ThrowIfArgumentNull(nameof(parsedInstances));
            ParseErrors = errors?.ToList();
        }

        public IList<ParseException> ParseErrors { get; set; }

        public IParseResult When<T>(Action<T> callback)
        {
            foreach (var parsedInstance in ParsedInstances.OfType<T>()) callback(parsedInstance);
            return this;
        }

        public IParseResult OnError(Action<IEnumerable<ParseException>> callback)
        {
            if (ParsedInstances.Any())
                return this;

            if (ParseErrors != null)
            {
                callback(ParseErrors.ToList());
            }

            return this;
        }

        public IList<object> ParsedInstances { get; set; }
    }
}