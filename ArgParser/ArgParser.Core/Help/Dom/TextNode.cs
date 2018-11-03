using System;

namespace ArgParser.Core.Help.Dom
{
    public class TextNode : HelpNode
    {
            
        public TextNode(string text)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

            
        public override void Accept(IHelpNodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public string Text { get; set; }
    }
}