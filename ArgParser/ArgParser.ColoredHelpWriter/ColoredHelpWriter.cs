using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alba.CsConsoleFormat;
using Alba.CsConsoleFormat.ColorfulConsole;
using ArgParser.Core.Help;
using ArgParser.Core.Help.Dom;
using Colorful;

namespace ArgParser.ColoredHelpWriter
{
    public class ColoredHelpWriter
    {
        
    }

    public class Theme
    {
        public ConsoleColor CodeColor { get; set; } = ConsoleColor.Green;
        public ConsoleColor TextColor { get; set; } = ConsoleColor.White;
    }

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

        /// <inheritdoc />
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
