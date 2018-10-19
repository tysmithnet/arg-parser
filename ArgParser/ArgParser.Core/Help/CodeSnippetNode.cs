namespace ArgParser.Core.Help
{
    public class CodeSnippetNode : LeafNode
    {
        /// <inheritdoc />
        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}