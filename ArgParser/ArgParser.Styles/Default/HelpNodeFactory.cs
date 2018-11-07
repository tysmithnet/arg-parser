using ArgParser.Core;
using ArgParser.Core.Help;

namespace ArgParser.Styles.Default
{
    public class HelpNodeFactory : IHelpNodeFactory
    {
        public RootNode Create(string parserId, IContext context)
        {
            var rootNode = new RootNode();
            return rootNode;
        }

        public virtual BlockNode CreateHeading(string parserId, IContext context)
        {
            var usageFac = new UsageFactory();

            return new BlockNode()
            {
                Children =
                {
                    new HeadingNode(parserId),
                    new TextNode("Usage: "),
                    usageFac.Create(parserId, context),
                }
            };
        }
    }
}