using System;
using System.Collections.Generic;
using System.Text;
using ArgParser.Core;

namespace ArgParser.Styles.Help
{
    public static class HelpExtensions
    {
        internal static IHelpNodeFactory HelpNodeFactory { get; set; } = new HelpNodeFactory();
        internal static IHelpWriter HelpWriter { get; set; } = new HelpWriter();

        public static void RenderHelp(this IContext context, string parserId, int width=80)
        {
            var node = HelpNodeFactory.Create(parserId, context);
            HelpWriter.RenderHelp(node, width);
        }
    }
}
