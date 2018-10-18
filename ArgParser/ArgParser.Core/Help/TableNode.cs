namespace ArgParser.Core.Help
{
    public class TableNode : InteriorNode
    {
        /// <inheritdoc />
        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}