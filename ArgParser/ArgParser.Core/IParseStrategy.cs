using System.Collections.Generic;
using System.Text;

namespace ArgParser.Core
{
    public interface IParseStrategy<T>
    {
        IParseResult Parse<TSub>(IEnumerable<IParser<T>> parsers, string[] args) where TSub : T;
    }
}
