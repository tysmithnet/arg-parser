using ArgParser.Core.Help.Dom;

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
        void Visit(HelpNode node);
        void Visit(ListNode node);
        void Visit(UnOrderedListNode node);
        void Visit(OrderedListNode node);
    }
}