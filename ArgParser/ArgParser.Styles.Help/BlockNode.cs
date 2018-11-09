namespace ArgParser.Styles.Help
{
    public class BlockNode : HelpNode
    {
        public override T Accept<T>(IHelpNodeVisitor<T> visitor) => visitor.Visit(this);
    }
}