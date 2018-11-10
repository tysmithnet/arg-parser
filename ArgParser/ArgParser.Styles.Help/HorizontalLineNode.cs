namespace ArgParser.Styles.Help
{
    public class HorizontalLineNode : HelpNode
    {
        public override T Accept<T>(IHelpNodeVisitor<T> visitor) => visitor.Visit(this);
    }
}