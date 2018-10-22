﻿namespace ArgParser.Core.Help
{
    public class TableDataNode : TextNode
    {
        /// <inheritdoc />
        public TableDataNode(string text) : base(text)
        {
        }

        /// <inheritdoc />
        public virtual void Accept(IHelpNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}