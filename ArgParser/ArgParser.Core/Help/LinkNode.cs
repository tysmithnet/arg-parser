namespace ArgParser.Core.Help
{
    public class LinkNode : LeafNode
    {
        /// <inheritdoc />
        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}