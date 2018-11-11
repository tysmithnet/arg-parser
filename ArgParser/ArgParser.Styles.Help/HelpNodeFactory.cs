using System;
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
            string version = parser.Help?.Version.IsNullOrWhiteSpace() ?? true ? "" : $" {parser.Help.Version}";
            var desc = parser.Help?.ShortDescription.IsNullOrWhiteSpace() ?? true ? "" : $" - {parser.Help.ShortDescription}";
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
                    new ColumnLength(),
                    new ColumnLength(1),
                }
            };
            grid.AddChild(new TextNode("Name"));
            grid.AddChild(new TextNode("Switch"));
            grid.AddChild(new TextNode("Num Values"));
            grid.AddChild(new TextNode("Description"));
            foreach (var parameter in parser.Parameters.OfType<Switch>())
            {
                var hi = parameter.MaxAllowed == int.MaxValue ? "N" : parameter.MaxAllowed.ToString();
                var range = parameter.MinRequired == parameter.MaxAllowed
                    ? $"{parameter.MinRequired}"
                    : $"{parameter.MinRequired}..{hi}";
                grid.AddChild(new TextNode(parameter.Help?.Name));
                grid.AddChild(new TextNode(parameter.ToString()));
                grid.AddChild(new TextNode(range));
                grid.AddChild(new TextNode(parameter.Help?.ShortDescription));
            }

            foreach (var parameter in inheritedParameters.OfType<Switch>())
            {
                var hi = parameter.MaxAllowed == int.MaxValue ? "N" : parameter.MaxAllowed.ToString();
                var range = parameter.MinRequired == parameter.MaxAllowed
                    ? $"{parameter.MinRequired}"
                    : $"{parameter.MinRequired}..{hi}";
                grid.AddChild(new TextNode(parameter.Help?.Name));
                grid.AddChild(new TextNode(parameter.ToString()));
                grid.AddChild(new TextNode(range));
                grid.AddChild(new TextNode($"*inherited* {parameter.Help?.ShortDescription}"));
            }

            int pos = 1;
            foreach (var positional in parser.Parameters.OfType<Positional>())
            {
                var hi = positional.MaxAllowed == int.MaxValue ? "N" : positional.MaxAllowed.ToString();
                var range = positional.MinRequired == positional.MaxAllowed
                    ? $"{positional.MinRequired}"
                    : $"{positional.MinRequired}..{hi}";
                grid.AddChild(new TextNode(positional.Help?.Name));
                grid.AddChild(new TextNode($"<pos {pos++}>"));
                grid.AddChild(new TextNode(range));
                grid.AddChild(new TextNode($"{positional.Help?.ShortDescription}"));
            }

            foreach (var positional in inheritedParameters.OfType<Positional>())
            {
                var hi = positional.MaxAllowed == int.MaxValue ? "N" : positional.MaxAllowed.ToString();
                var range = positional.MinRequired == positional.MaxAllowed
                    ? $"{positional.MinRequired}"
                    : $"{positional.MinRequired}..{hi}";
                grid.AddChild(new TextNode(positional.Help?.Name));
                grid.AddChild(new TextNode($"<pos {pos++}>"));
                grid.AddChild(new TextNode(range));
                grid.AddChild(new TextNode($"*inherited* {positional.Help?.ShortDescription}"));
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
                    new ColumnLength(1),
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