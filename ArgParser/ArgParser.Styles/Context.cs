using ArgParser.Core;

namespace ArgParser.Styles
{
    public class Context : IContext
    {
        public IHierarchyRepository HierarchyRepository { get; protected internal set; } = new HierarchyRepository();
        public IParserRepository ParserRepository { get; protected internal set; } = new ParserRepository();
        public IAliasRepository AliasRepository { get; protected internal set; } = new AliasRepository();
    }
}