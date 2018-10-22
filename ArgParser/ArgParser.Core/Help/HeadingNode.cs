namespace ArgParser.Core.Help
{
    public class HeadingNode : TextNode
    {
        public int Size { get; set; }

        /// <inheritdoc />
        public override void Accept(IHelpNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}