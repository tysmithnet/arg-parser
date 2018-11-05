using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class Context : IContext
    {
        public IParserRepository ParserRepository { get; protected internal set; } = new ParserRepository();

        public IHierarchyRepository HierarchyRepository { get; protected internal set; } = new HierarchyRepository();
    }
}