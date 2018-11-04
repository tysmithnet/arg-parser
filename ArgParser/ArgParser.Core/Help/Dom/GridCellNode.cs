namespace ArgParser.Core.Help.Dom
{
    public class GridCellNode : HelpNode
    {
        /// <inheritdoc />
        public override T Accept<T>(IHelpNodeVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}