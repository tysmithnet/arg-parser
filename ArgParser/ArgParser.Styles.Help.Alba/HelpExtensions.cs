using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ArgParser.Core;
using Colorful;

namespace ArgParser.Styles.Help.Alba
{
    public static class HelpExtensions
    {
        internal static Dictionary<ContextBuilder, HelpRenderer> Renderers { get; set; } = new Dictionary<ContextBuilder, HelpRenderer>();

        public static void RenderHelp(this IContext context, string parserId, int width = 80)
        {
            var renderer = new HelpRenderer();
            renderer.Render(context, parserId, width);
        }

        public static ContextBuilder AddMultiColoredCommands(this ContextBuilder builder, params Color[] colors)
        {
            var convertedColors = colors.PreventNull().Select(c => c.ToNearestConsoleColor()).ToArray();
            return builder;
        }
    }

    public class ColorfulSubCommandDecorator : HelpNodeFactory
    {

    }

    public class HelpRenderer
    {
        public IHelpNodeFactory HelpNodeFactory { get; set; } = new HelpNodeFactory();
        public IHelpWriter HelpWriter { get; set; } = new HelpWriter();

        public void Render(IContext context, string parserId, int width)
        {
            var root = HelpNodeFactory.Create(parserId, context);
            HelpWriter.RenderHelp(root, width);
        }
    }
}