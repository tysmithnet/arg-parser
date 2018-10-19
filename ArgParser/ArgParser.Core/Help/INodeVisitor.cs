namespace ArgParser.Core.Help
{
    public interface INodeVisitor
    {
        void Visit(ParagraphNode node);
        void Visit(HeadingNode node);
        void Visit(OrderedListNode node);
        void Visit(UnorderedListNode node);
        void Visit(CodeSnippetNode node);
        void Visit(CodeBlockNode node);
        void Visit(TextSnippetNode node);
        void Visit(LinkNode node);
        void Visit(TableNode node);
        void Visit(TableRowNode node);
        void Visit(TableCellNode node);
    }
}