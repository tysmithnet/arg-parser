namespace ArgParser.Core.Help
{
    public class TextSnippetNode : LeafNode
    {
        /// <inheritdoc />
        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}