namespace ArgParser.Styles.Help
{
    public class BlockNode : HelpNode
    {
        public override T Accept<T>(IHelpNodeVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
