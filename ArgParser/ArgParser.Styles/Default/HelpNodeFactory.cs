using System;
using System.Collections.Generic;
using System.Text;
using ArgParser.Core;
using ArgParser.Core.Help;

namespace ArgParser.Styles.Default
{
    public class HelpNodeFactory : IHelpNodeFactory
    {
        public RootNode Create(IContext context, string parserId)
        {
            var parser = context.ParserRepository.Get(parserId);
            var children = context.HierarchyRepository.GetChildren(parserId);

            var rootNode = new RootNode();
            if (parser.Help != null)
            {

            }
            

            return rootNode;
        }
    }
}
