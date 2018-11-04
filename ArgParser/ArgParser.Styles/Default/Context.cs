using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class Context : IContext
    {
        public IParserRepository ParserRepository { get; } = new ParserRepository();

        public IHierarchyRepository HierarchyRepository { get; } = new HierarchyRepository();
    }
}