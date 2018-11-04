using System;

namespace ArgParser.Core.Help.Dom
{
    public class TextNode : HelpNode
    {
            
        public TextNode(string text)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }


        public string Text { get; set; }

        /// <inheritdoc />
        public override T Accept<T>(IHelpNodeVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}