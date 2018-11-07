namespace ArgParser.Core.Help
{
    public class TextNode : HelpNode
    {
        public TextNode(string text)
        {
            Text = text.ThrowIfArgumentNull(nameof(text));
        }

        public string Text { get; protected internal set; }
    }

    public class CodeNode : TextNode
    {
        public CodeNode(string text) : base(text)
        {
        }
    }
}