using System.Collections.Generic;
using System.Text;

namespace ArgParser.Core
{
    public interface IParseStrategy
    {
        IParseResult Parse(IEnumerable<IParser> parsers, string[] args);
    }
}
