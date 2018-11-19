namespace ArgParser.Core
{
    public interface IContext
    {
        IHierarchyRepository HierarchyRepository { get; }
        IParserRepository ParserRepository { get; }
        IAliasRepository AliasRepository { get; }
    }
}