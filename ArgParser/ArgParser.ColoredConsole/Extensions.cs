using Alba.CsConsoleFormat;
using ArgParser.Core.Help.Dom;

namespace ArgParser.ColoredConsole
{
    public static class Extensions
    {
        public static void PrintHelp(this RootNode root)
        {
            var visitor = new ColorfulHelpVisitor();
            ConsoleRenderer.RenderDocument(visitor.Create(root));
        }
    }
}