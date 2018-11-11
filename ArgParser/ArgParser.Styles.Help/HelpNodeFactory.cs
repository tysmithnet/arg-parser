using System.Linq;
using ArgParser.Core;
using ArgParser.Styles.Default;

namespace ArgParser.Styles.Help
{
    public class HelpNodeFactory : IHelpNodeFactory
    {
        public virtual RootNode Create(string parserId, IContext context)
        {
            parserId.ThrowIfArgumentNull(nameof(parserId));
            context.ThrowIfArgumentNull(nameof(context));
            var rootNode = new RootNode
            {
                Children =
                {
                    CreateHeading(parserId, context),
                    new HorizontalLineNode(),
                    CreateDescription(parserId, context),
                    new HorizontalLineNode(),
                    CreateSubCommands(parserId, context),
                    new HorizontalLineNode(),
                    CreateParameters(parserId, context)
                }
            };

            return rootNode;
        }

        public virtual HelpNode CreateDescription(string parserId, IContext context)
        {
            var parser = context.ParserRepository.Get(parserId);
            return Block(
                Block(
                    new TextNode("Usage: "),
                    UsageFactory.Create(parserId, context)
                ),
                new TextNode(parser.Help?.LongDescription)
            );
        }

        public virtual HelpNode CreateHeading(string parserId, IContext context) => new HeadingNode(parserId);

        public virtual HelpNode CreateParameters(string parserId, IContext context)
        {
            var parser = context.ParserRepository.Get(parserId);

            var grid = new GridNode
            {
                Columns =
                {
                    new ColumnLength(),
                    new ColumnLength(1)
                }
            };
            grid.AddChild(new TextNode("Parameter"));
            grid.AddChild(new TextNode("Description"));
            foreach (var parameter in parser.Parameters.OfType<Switch>())
            {
                grid.AddChild(new TextNode(parameter.ToString()));
                grid.AddChild(new TextNode(parameter.Help?.ShortDescription));
            }

            return grid;
        }

        public virtual HelpNode CreateSubCommands(string parserId, IContext context)
        {
            var children = context.HierarchyRepository.GetChildren(parserId)
                .Select(x => context.ParserRepository.Get(x));
            var grid = new GridNode
            {
                Columns =
                {
                    new ColumnLength(),
                    new ColumnLength(1)
                }
            };
            grid.AddChild(new TextNode("SubCommand"));
            grid.AddChild(new TextNode("Description"));
            foreach (var child in children)
            {
                grid.AddChild(new TextNode(child.Id));
                grid.AddChild(new TextNode(child.Help?.ShortDescription));
            }

            return grid;
        }

        private BlockNode Block(params HelpNode[] children)
        {
            var val = new BlockNode();
            foreach (var helpNode in children.PreventNull()) val.AddChild(helpNode);
            return val;
        }

        public IUsageFactory UsageFactory { get; set; } = new UsageFactory();
    }
}