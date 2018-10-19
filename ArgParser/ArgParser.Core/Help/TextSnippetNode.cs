using System;

namespace ArgParser.Core.Help
{
    public class TextSnippetNode : LeafNode
    {
        public TextSnippetNode(string text)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        /// <inheritdoc />
        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}