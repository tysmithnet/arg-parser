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
        internal static Dictionary<Parser, Theme> Themes { get; set; } = new Dictionary<Parser, Theme>();

        public static void RenderHelp(this ContextBuilder contextBuilder, string parserId, int width = 80)
        {
            var renderer = new HelpRenderer();
            renderer.Render(contextBuilder.BuildContext(), parserId, width);
        }

        public static ContextBuilder AddRenderSupport(this ContextBuilder builder)
        {
            if(!Renderers.ContainsKey(builder))
                Renderers.Add(builder, new HelpRenderer());
            return builder;
        }

        public static ContextBuilder AddMultiColoredCommands(this ContextBuilder builder, params Color[] colors)
        {
            var convertedColors = colors.PreventNull().Select(c => c.ToNearestConsoleColor()).ToArray();
            var renderer = Renderers[builder];
            renderer.HelpNodeFactory.ParameterHelpCreated += (sender, args) =>
            {
                var parser = args.Parameter.Parent;
                if (parser == null)
                    return;

            }
            return builder;
        }
    }

    public class 

    public class HelpRenderer
    {
        public HelpNodeFactory HelpNodeFactory { get; set; } = new HelpNodeFactory();
        public HelpWriter HelpWriter { get; set; } = new HelpWriter();

        public void Render(IContext context, string parserId, int width)
        {
            var root = HelpNodeFactory.Create(parserId, context);
            HelpWriter.RenderHelp(root, width);
        }
    }
}