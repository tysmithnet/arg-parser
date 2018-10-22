namespace ArgParser.Core.Help
{
    public class CodeNode : TextNode
    {
        /// <inheritdoc />
        public CodeNode(string text) : base(text)
        {
        }

        /// <inheritdoc />
        public override void Accept(IHelpNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}