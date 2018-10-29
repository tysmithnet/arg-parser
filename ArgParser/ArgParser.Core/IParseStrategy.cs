using System.Collections.Generic;

namespace ArgParser.Core
{
    public interface IParseStrategy
    {
        IParseResult Parse(IEnumerable<IParser> parsers, string[] args);
    }

    public interface IParseStrategy<out T> : IParseStrategy
    {
        IParseResult Parse(IEnumerable<IParser<T>> parsers, string[] args);
    }
}