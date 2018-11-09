namespace ArgParser.HelpWriter
{
    public interface IHelpNodeVisitor<out T>
    {
        T Visit(HelpNode node);
        T Visit(RootNode node);
        T Visit(TextNode node);
        T Visit(HeadingNode node);
        T Visit(BlockNode node);
        T Visit(HorizontalLineNode node);
        T Visit(GridNode node);
        T Visit(CodeNode node);
    }
}