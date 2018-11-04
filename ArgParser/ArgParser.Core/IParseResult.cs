using System;
using System.Collections.Generic;
using ArgParser.Core.Validation;

namespace ArgParser.Core
{
    public interface IParseResult
    {
        IParseResult When<T>(Action<T> callback); // todo: maybe add error parameter to callback
        IParseResult OnError(Action<IEnumerable<ParseException>> callback);
    }
}