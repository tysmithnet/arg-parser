using System;
using System.Collections.Generic;

namespace ArgParser.Core
{
    public interface IParseResult
    {
        void When<T>(Action<T, Parser> handler);
        void WhenError(Action<IEnumerable<ParseException>> handler);
    }
}