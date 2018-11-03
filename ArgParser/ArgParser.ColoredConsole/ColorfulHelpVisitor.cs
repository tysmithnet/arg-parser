using System;
using System.Linq;
using Alba.CsConsoleFormat;
using ArgParser.Core.Help;
using ArgParser.Core.Help.Dom;

namespace ArgParser.ColoredConsole
{
    public class ColorfulHelpVisitor : IHelpNodeVisitor<Element>
    {
        /// <inheritdoc />
        public Element Visit(TextNode node)
        {
            return new Span(node.Text)
            {
                Color = ConsoleColor.Green
            };
        }

        /// <inheritdoc />
        public Element Visit(CodeNode node)
        {
            return new Span(node.Text)
            {
                Color = ConsoleColor.Yellow
            };
        }

        /// <inheritdoc />
        public Element Visit(HeadingNode node)
        {
            return new Span(node.Text)
            {
                Color = ConsoleColor.Magenta
            };
        }

        /// <inheritdoc />
        public new Element Visit(RootNode node)
        {
            var children = node.Children.Select(x => x.Accept(this));
            return new Document(children);
        }
    }
}