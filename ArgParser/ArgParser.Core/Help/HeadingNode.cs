namespace ArgParser.Core.Help
{
    public class HeadingNode : InteriorNode
    {
        /// <inheritdoc />
        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}