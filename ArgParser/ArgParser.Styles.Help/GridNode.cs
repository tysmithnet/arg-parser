namespace ArgParser.Styles.Help
{
    public class GridNode : HelpNode
    {
        public int Columns { get; set; } = 1;

        public override T Accept<T>(IHelpNodeVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}