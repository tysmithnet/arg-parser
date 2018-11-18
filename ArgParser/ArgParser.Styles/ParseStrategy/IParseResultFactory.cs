using System.Collections.Generic;
using ArgParser.Core;

namespace ArgParser.Styles.ParseStrategy
{
    public interface IParseResultFactory : IParseStrategyUnit
    {
        IParseResult Create(Dictionary<object, Parser> results, IEnumerable<ParseException> parseExceptions);
    }
}