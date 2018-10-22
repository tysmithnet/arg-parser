namespace ArgParser.Core.Help
{
    public class TextNode : IHelpNode
    {
        public string Text { get; set; }
        /// <inheritdoc />
        public virtual void Accept(IHelpNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}