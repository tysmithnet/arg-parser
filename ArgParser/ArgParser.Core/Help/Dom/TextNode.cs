using System;

namespace ArgParser.Core.Help.Dom
{
    public class TextNode : IHelpNode
    {
            
        public TextNode(string text)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

            
        public virtual void Accept(IHelpNodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public string Text { get; set; }
    }
}