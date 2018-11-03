﻿namespace ArgParser.Core.Help.Dom
{
    public class HeadingNode : TextNode
    {
            
        public HeadingNode(string text) : base(text)
        {
        }

            
        public override void Accept(IHelpNodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public int Size { get; set; }
    }
}