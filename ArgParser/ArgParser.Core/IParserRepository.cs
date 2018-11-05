using System.Collections.Generic;

namespace ArgParser.Core
{
    public interface IParserRepository
    {
        Parser Create(string id);
        Parser Get(string id);
        IEnumerable<Parser> GetAll();
    }
}