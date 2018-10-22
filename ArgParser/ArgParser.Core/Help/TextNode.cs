﻿using System;

namespace ArgParser.Core.Help
{
    public class TextNode : IHelpNode
    {
        /// <inheritdoc />
        public TextNode(string text)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        /// <inheritdoc />
        public virtual void Accept(IHelpNodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public string Text { get; set; }
    }
}