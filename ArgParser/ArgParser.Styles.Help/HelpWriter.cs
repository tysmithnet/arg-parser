using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alba.CsConsoleFormat;
using Alba.CsConsoleFormat.ColorfulConsole;
using ArgParser.Core;

namespace ArgParser.Styles.Help
{
    public class HelpWriter
    {
        public string CreateHelpText(RootNode rootNode, int width = 80)
        {
            var visitor = new HelpWriterVisitor();
            var doc = (Document)visitor.Visit(rootNode);
            string text = ConsoleRenderer.RenderDocumentToText(doc, new TextRenderTarget(),
                new Rect(0, 0, width, Size.Infinity));
            return text.Length > 0 ? text.Remove(text.Length - 2) : text;
        }
    }

    public class HelpWriterVisitor : IHelpNodeVisitor<Element>
    {
        public Element Default(HelpNode node)
        {
            var span = new Span();
            foreach (var child in node.Children)
            {
                var element = child.Accept(this);
                span.Children.Add(element);
            }

            return span;
        }

        public Element Visit(HelpNode node)
        {
            return Default(node);
        }

        public Element Visit(RootNode node)
        {
            var children = node.Children.Select(x => x.Accept(this));
            // ReSharper disable once CoVariantArrayConversion
            return new Document(children.ToArray());
        }

        public Element Visit(TextNode node)
        {
            return new Span()
            {
                Text = node.Text
            };
        }

        public Element Visit(HeadingNode node)
        {
            return new FigletDiv()
            {
                Text = node.Text
            };
        }

        public Element Visit(BlockNode node)
        {
            var div = new Div();
            foreach (var child in node.Children)
            {
                div.Children.Add(child.Accept(this));
            }
            return div;
        }

        public Element Visit(HorizontalLineNode node)
        {
            return new Div(new Span("------------------------------"));
        }

        public Element Visit(GridNode node)
        {
            var grid = new Grid()
            {
                Columns = { Enumerable.Range(0, node.Columns).Select(x => GridLength.Auto)}
            };
            foreach (var child in node.Children)
            {
                grid.Children.Add(new Cell(child.Accept(this)));
            }

            return grid;
        }

        public Element Visit(CodeNode node)
        {
            return new Span(node.Text);
        }
    }
}
