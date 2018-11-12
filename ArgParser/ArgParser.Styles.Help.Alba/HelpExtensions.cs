using ArgParser.Core;

namespace ArgParser.Styles.Help.Alba
{
    public static class HelpExtensions
    {
        public static void RenderHelp(this IContext context, string parserId, int width = 80)
        {
            var node = HelpNodeFactory.Create(parserId, context);
            HelpWriter.RenderHelp(node, width);
        }

        internal static IHelpNodeFactory HelpNodeFactory { get; set; } = new HelpNodeFactory();
        internal static IHelpWriter HelpWriter { get; set; } = new HelpWriter();
    }
}