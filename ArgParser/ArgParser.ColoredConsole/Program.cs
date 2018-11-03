using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Alba.CsConsoleFormat;
using ArgParser.Core.Help.Dom;

namespace ArgParser.ColoredConsole
{
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
