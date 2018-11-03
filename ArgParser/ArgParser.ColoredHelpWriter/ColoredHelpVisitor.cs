using System;
using Alba.CsConsoleFormat;
using Alba.CsConsoleFormat.ColorfulConsole;
using ArgParser.Core.Help;
using ArgParser.Core.Help.Dom;

namespace ArgParser.ColoredHelpWriter
{
    public class ColoredHelpVisitor : IHelpNodeVisitor<Element>
    {
        public Theme Theme { get; set; } = new Theme();

        public Element Visit(TextNode node)
        {
            return new Span(node.Text)
            {
                Color = Theme.TextColor
            };
        }

        public Element Visit(CodeNode node)
        {
            return new Span(node.Text)
            {
                Color = Theme.CodeColor
            };
        }

        public Element Visit(HeadingNode node)
        {
            return new FigletDiv()
            {
                Text = node.Text,
            };
        }
        public Element Visit(RootNode node)
        {
            throw new NotImplementedException();
        }

        public Element Visit(HelpNode node)
        {
            throw new NotImplementedException();
        }

        public Element Visit(ListNode node)
        {
            throw new NotImplementedException();
        }

    }
}