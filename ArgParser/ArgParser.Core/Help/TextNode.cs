namespace ArgParser.Core.Help
{
    public abstract class TextNode : HelpNode
    {
        protected internal TextNode(string text)
        {
            Text = text.ThrowIfArgumentNull(nameof(text));
        }

        public string Text { get; protected internal set; }
    }
}