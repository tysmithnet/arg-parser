using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Alba.CsConsoleFormat;
using ArgParser.Core.Help;
using ArgParser.Core.Help.Dom;

namespace ArgParser.ColoredConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var root = new RootNode();
            root.Add(new HeadingNode("hello"));
            var grid = new GridNode();
            grid.NumColumns = 2;
            grid.Add(new TextNode("d0"));
            grid.Add(new CodeNode("d1"));
            grid.Add(new TextNode("d2"));
            grid.Add(new CodeNode("d3"));
            root.Add(grid);
            var visitor = new ColorfulHelpVisitor();
            var doc = (Document)root.Accept(visitor);
            string text = ConsoleRenderer.RenderDocumentToText(doc, new TextRenderTarget(),
                new Rect(0, 0, Console.WindowWidth, Size.Infinity));
            
            Console.ReadKey();
        }
    }
}
