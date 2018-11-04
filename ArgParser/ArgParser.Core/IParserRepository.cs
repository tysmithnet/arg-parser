using System.Collections.Generic;

namespace ArgParser.Core
{
    public interface IParserRepository
    {
        Parser Create(string id);
        void AddParser(string id, Parser parser);
        Parser GetParser(string id);
        IEnumerable<Parser> GetAll();
    }
}