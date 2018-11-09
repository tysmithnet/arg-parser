using ArgParser.Core;

namespace ArgParser.HelpWriter
{
    public class TextNode : HelpNode
    {
        public TextNode(string text)
        {
            Text = text.ThrowIfArgumentNull(nameof(text));
        }

        public string Text { get; protected internal set; }
    }
}