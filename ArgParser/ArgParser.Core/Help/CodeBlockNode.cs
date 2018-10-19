namespace ArgParser.Core.Help
{
    public class CodeBlockNode : LeafNode
    {
        /// <inheritdoc />
        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}