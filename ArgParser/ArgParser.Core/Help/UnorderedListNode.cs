namespace ArgParser.Core.Help
{
    public class UnorderedListNode : ListNode
    {
        /// <inheritdoc />
        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}