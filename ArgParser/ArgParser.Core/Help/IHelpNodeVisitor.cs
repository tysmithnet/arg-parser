using ArgParser.Core.Help.Dom;

namespace ArgParser.Core.Help
{
    public interface IHelpNodeVisitor<out T>
    {
        T Visit(TextNode node);
        T Visit(CodeNode node);
        T Visit(HeadingNode node);
        T Visit(RootNode node);
        T Visit(HelpNode node);
        T Visit(ListNode node);
        T Visit(GridNode node);
        T Visit(GridCellNode node);
        T Visit(BlockNode node);
    }
}