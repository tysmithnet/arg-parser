using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public class Program
    {
        public static void Main(string[] args)
        {
            var root = new RootNode();
            root.Add(new HeadingNode("hello"));
            var visitor = new ColorfulHelpVisitor();
            var doc = (Document)root.Accept(visitor);
            ConsoleRenderer.RenderDocument(doc);
            Console.ReadKey();
        }
    }
}
