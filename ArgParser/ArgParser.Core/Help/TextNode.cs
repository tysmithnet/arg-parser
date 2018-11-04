namespace ArgParser.Core.Help
{
    public abstract class TextNode : HelpNode
    {
        public string Text { get; protected internal set; }

        protected internal TextNode(string text)
        {
            Text = text.ThrowIfArgumentNull(nameof(text));
        }
    }
}