﻿namespace ArgParser.Core.Help
{
    public class HeadingNode : TextNode
    {
        /// <inheritdoc />
        public HeadingNode(string text) : base(text)
        {
        }

        /// <inheritdoc />
        public override void Accept(IHelpNodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public int Size { get; set; }
    }
}