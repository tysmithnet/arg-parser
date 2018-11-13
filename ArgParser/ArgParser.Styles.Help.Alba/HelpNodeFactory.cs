using System;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles.Help.Alba
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
                    UsageFactory.CreateFullUsage(parserId, context)
                ),
                new TextNode(parser.Help?.LongDescription)
            );
        }

        public virtual HelpNode CreateHeading(string parserId, IContext context)
        {
            var parser = context.ParserRepository.Get(parserId);
            var displayName = parser.Help?.Name ?? parserId;
            var heading = new HeadingNode(displayName);
            var version = parser.Help?.Version.IsNullOrWhiteSpace() ?? true ? "" : $" {parser.Help.Version}";
            var desc = parser.Help?.ShortDescription.IsNullOrWhiteSpace() ?? true
                ? ""
                : $" - {parser.Help.ShortDescription}";
            var subHeading = $"{displayName}{version}{desc}";
            return Block(heading, Block(new TextNode(subHeading)));
        }

        public virtual HelpNode CreateParameters(string parserId, IContext context)
        {
            var parser = context.ParserRepository.Get(parserId);
            var inheritedParameters = context
                .HierarchyRepository
                .GetAncestors(parserId)
                .Select(x => context.ParserRepository.Get(x))
                .SelectMany(x => x.Parameters)
                .ToList();
            var grid = new GridNode
            {
                Columns =
                {
                    new ColumnLength(),
                    new ColumnLength(),
                    new ColumnLength(1)
                }
            };
            grid.AddChild(new TextNode("Name"));
            grid.AddChild(new TextNode("Usage"));
            grid.AddChild(new TextNode("Description"));

            foreach (var sw in parser.Parameters.OfType<Switch>().Concat(inheritedParameters.OfType<Switch>()))
            {
                grid.AddChild(new TextNode(sw.Help?.Name));
                grid.AddChild(UsageFactory.CreateSwitchUsage(sw, context));
                grid.AddChild(new TextNode($"{sw.Help?.ShortDescription}"));
            }

            foreach (var pos in parser.Parameters.OfType<Positional>().Concat(inheritedParameters.OfType<Positional>()))
            {
                grid.AddChild(new TextNode(pos.Help?.Name));
                grid.AddChild(UsageFactory.CreatePositionalUsage(pos, context));
                var prefix = pos.IsRequired ? $"*req* " : "";
                grid.AddChild(new TextNode($"{prefix}{pos.Help?.ShortDescription}"));
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