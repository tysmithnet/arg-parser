using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgParser.ColoredConsole;
using ArgParser.Core.Help.Dom;

namespace ArgParser.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var help = new RootNode();
            help.Add(new HeadingNode("hello world"));
            help.PrintHelp();
            Console.ReadKey();
        }
    }
}
