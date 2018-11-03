﻿using System;
using System.Collections.Generic;
using System.Linq;
using Alba.CsConsoleFormat;
using Alba.CsConsoleFormat.ColorfulConsole;
using ArgParser.Core.Help;
using ArgParser.Core.Help.Dom;

namespace ArgParser.ColoredConsole
{
    public class Theme
    {
        public ConsoleColor TextColor { get; set; } = ConsoleColor.White;
        public ConsoleColor CodeColor { get; set; } = ConsoleColor.Green;
        public ConsoleColor HeadingColor { get; set; } = ConsoleColor.Cyan;
    }

    public class ColorfulHelpVisitor : IHelpNodeVisitor<Element>
    {
        public Theme Theme { get; set; } = new Theme();

        /// <inheritdoc />
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
                Color = Theme.HeadingColor
            };
        }

        /// <inheritdoc />
        public Element Visit(RootNode node)
        {
            var children = GetChildren(node);
            return new Document(children);
        }

        /// <inheritdoc />
        public Element Visit(GridNode node)
        {
            var children = GetChildren(node);
            return new Grid(children)
            {
                Columns = { Enumerable.Range(0, node.NumColumns).Select(x => GridLength.Auto).ToArray()}
            };
        }

        protected internal IEnumerable<Element> GetChildren(IHelpNode node)
        {
            return node.Children.Select(x => x.Accept(this));
        }
    }
}