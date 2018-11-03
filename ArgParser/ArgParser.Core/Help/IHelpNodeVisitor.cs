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

    public interface IHelpNodeVisitor<out T>
    {
        T Visit(TextNode node);
        T Visit(CodeNode node);
        T Visit(HeadingNode node);
        T Visit(TableNode node);
        T Visit(TableRowNode node);
        T Visit(TableDataNode node);
        T Visit(RootNode node);
        T Visit(HelpNode node);
        T Visit(ListNode node);
        T Visit(UnOrderedListNode node);
        T Visit(OrderedListNode node);
    }
}