using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class Context : IContext
    {
        public IHierarchyRepository HierarchyRepository { get; protected internal set; } = new HierarchyRepository();
        public IParserRepository ParserRepository { get; protected internal set; } = new ParserRepository();
    }
}