namespace ArgParser.Core.Help
{
    public interface IHelpNodeVisitor
    {
        void Visit(TextNode node);
        void Visit(CodeNode node);
        void Visit(HeadingNode node);
        void Visit(TableNode node);
        void Visit(TableRowNode node);
        void Visit(TableDataNode node);
        void Visit(RootNode node);
    }
}