using System.Collections.Generic;
using System.Linq;
using Alba.CsConsoleFormat;
using Alba.CsConsoleFormat.ColorfulConsole;
using ArgParser.Core;
using ArgParser.Core.Help;
using ArgParser.Core.Help.Dom;

namespace ArgParser.ColoredConsole
{
    public class ColorfulHelpVisitor : IHelpNodeVisitor<Element>
    {
        public Theme Theme { get; set; } = new Theme();

        public Document Create(RootNode rootNode)
        {
            rootNode.ThrowIfArgumentNull(nameof(rootNode));
            return (Document) rootNode.Accept(this);
        }

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
                Columns = { Enumerable.Range(0, node.NumColumns).Select(x => GridLength.Auto).ToArray() }
            };
        }

        /// <inheritdoc />
        public Element Visit(GridCellNode node)
        {
            var children = GetChildren(node);
            return new Cell(children);
        }

        protected internal IEnumerable<Element> GetChildren(IHelpNode node)
        {
            return node.Children.Select(x => x.Accept(this));
        }
    }


    public static class Extensions
    {
        public static void PrintHelp(this RootNode root)
        {
            var visitor = new ColorfulHelpVisitor();
            ConsoleRenderer.RenderDocument(visitor.Create(root));
        }
    }
}