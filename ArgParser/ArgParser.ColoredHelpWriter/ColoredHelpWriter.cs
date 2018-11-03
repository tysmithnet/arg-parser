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

        /// <inheritdoc />
        public Element Visit(HeadingNode node)
        {
            return new FigletDiv()
            {
                Text = node.Text,
            };
        }

        /// <inheritdoc />
        public Element Visit(TableNode node)
        {
            var children = node.Children.Select(x => x.Accept(this));
            return new Grid(children);
        }

        /// <inheritdoc />
        public Element Visit(TableRowNode node)
        {
            return null;
        }

        /// <inheritdoc />
        public Element Visit(TableDataNode node)
        {
            
        }

        /// <inheritdoc />
        public Element Visit(RootNode node)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Element Visit(HelpNode node)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Element Visit(ListNode node)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Element Visit(UnOrderedListNode node)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Element Visit(OrderedListNode node)
        {
            throw new NotImplementedException();
        }
    }
}
