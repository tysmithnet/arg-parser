namespace ArgParser.Core.Help
{
    public class OrderedListNode : ListNode
    {
        /// <inheritdoc />
        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}