namespace ArgParser.Core.Help
{
    public class ParagraphNode : InteriorNode
    {
        /// <inheritdoc />
        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}