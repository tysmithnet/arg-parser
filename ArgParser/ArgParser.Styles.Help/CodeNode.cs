﻿namespace ArgParser.Styles.Help
{
    public class CodeNode : TextNode
    {
        public CodeNode(string text) : base(text)
        {
        }

        public override T Accept<T>(IHelpNodeVisitor<T> visitor) => visitor.Visit(this);
    }

    public class SpanNode : HelpNode
    {
    }
}