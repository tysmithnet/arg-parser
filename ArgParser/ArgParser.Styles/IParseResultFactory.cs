using System.Collections.Generic;
using ArgParser.Core;

namespace ArgParser.Styles
{
    public interface IParseResultFactory
    {
        IParseResult Create(Dictionary<object, Parser> results, IEnumerable<ParseException> parseExceptions);
    }
}