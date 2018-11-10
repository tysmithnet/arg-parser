using System.Collections.Generic;

namespace ArgParser.Core
{
    public interface IParserRepository
    {
        Parser Root { get; }
        Parser Create(string id, bool isRoot = false);
        Parser<T> Create<T>(string id, bool isRoot = false);
        Parser Get(string id);
        Parser<T> Get<T>(string id);
        IEnumerable<Parser> GetAll();
    }
}