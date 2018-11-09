using ArgParser.Core;

namespace ArgParser.Styles.Help
{
    public class TextNode : HelpNode
    {
        public TextNode(string text)
        {
            Text = text.ThrowIfArgumentNull(nameof(text));
        }

        public string Text { get; protected internal set; }

        public override T Accept<T>(IHelpNodeVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}