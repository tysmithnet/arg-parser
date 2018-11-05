namespace ArgParser.Core.Help
{
    public interface IHelpNodeVisitor<out T>
    {
        T Visit(HelpNode node);
        T Visit(RootNode node);
        T Visit(TextNode node);
        T Visit(HeadingNode node);
    }
}