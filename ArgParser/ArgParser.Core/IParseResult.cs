using System;
using System.Collections.Generic;
using ArgParser.Core.Validation;

namespace ArgParser.Core
{
    public interface IParseResult
    {
        IParseResult When<T>(Action<T> callback);
        IParseResult OnError(Action<IEnumerable<ParseError>> callback);
    }
}