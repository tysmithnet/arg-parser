namespace ArgParser.Styles.Help
{
    public class TextNode : HelpNode
    {
        public TextNode(string text)
        {
            Text = text ?? "";
        }

        public override T Accept<T>(IHelpNodeVisitor<T> visitor) => visitor.Visit(this);

        public string Text { get; protected internal set; }
    }
}