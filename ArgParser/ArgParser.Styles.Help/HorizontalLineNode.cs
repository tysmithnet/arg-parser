namespace ArgParser.Styles.Help
{
    public class HorizontalLineNode : HelpNode
    {
        public override T Accept<T>(IHelpNodeVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
