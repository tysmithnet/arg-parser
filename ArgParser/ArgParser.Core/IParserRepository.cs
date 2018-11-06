using System.Collections.Generic;

namespace ArgParser.Core
{
    public interface IParserRepository
    {
        Parser Get(string id);
        Parser Create(string id);
        Parser<T> Create<T>(string id);
        Parser<T> Get<T>(string id);
        IEnumerable<Parser> GetAll();
    }
}