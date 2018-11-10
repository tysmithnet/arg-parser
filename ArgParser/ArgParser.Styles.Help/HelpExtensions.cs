using System;
using System.Collections.Generic;
using System.Text;
using ArgParser.Core;

namespace ArgParser.Styles.Help
{
    public static class HelpExtensions
    {
        public static void RenderHelp(this IContext context, string parserId)
        {
            var help = new HelpWriter();
            var fac = new HelpNodeFactory();
            var root = fac.Create(parserId, context);
            help.RenderHelp(root);
        }
    }
}
