using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alba.CsConsoleFormat;

namespace ArgParser.ColoredConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var doc = new Document(new Span("hello"){Color = ConsoleColor.Red});
            ConsoleRenderer.RenderDocument(doc);
            Console.ReadKey();
        }
    }
}
