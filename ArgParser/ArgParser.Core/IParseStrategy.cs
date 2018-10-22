using System.Collections.Generic;

namespace ArgParser.Core
{
    public interface IParseStrategy
    {
        IParseResult Parse(IEnumerable<IParser> parsers, string[] args);
    }
}