using System;
using System.Collections.Generic;
using System.Text;
using ArgParser.Core;
using ArgParser.Core.Help;

namespace ArgParser.Styles.Default
{
    public class HelpNodeFactory : IHelpNodeFactory
    {
        public RootNode Create(string parserId, IContext context)
        {
            var rootNode = new RootNode();
            var usageFac = new UsageFactory();
            rootNode.AddChild(usageFac.Create(parserId, context));
            return rootNode;
        }
    }
}
