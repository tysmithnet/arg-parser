using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;
using ArgParser.Styles.Default;

namespace ArgParser.Styles.Help
{
    public class UsageFactory : IUsageFactory
    {
        public HelpNode CreateFullUsage(string parserId, IContext context)
        {
            parserId.ThrowIfArgumentNull(nameof(parserId));
            context.ThrowIfArgumentNull(nameof(context));
            var span = new SpanNode();
            var parser = context.ParserRepository.Get(parserId);
            var allPossibleParameters = parser.Parameters.Concat(context
                    .HierarchyRepository
                    .GetAncestors(parserId)
                    .Select(x => context.ParserRepository.Get(parserId))
                    .SelectMany(x => x.Parameters))
                .ToList();

            var switches = allPossibleParameters.OfType<Switch>().ToList();
            var positionals = allPossibleParameters.OfType<Positional>().ToList();
            foreach (var parameter in switches) span.AddChild(CreateSwitchUsage(parameter, context));
            foreach (var positional in positionals) span.AddChild(CreatePositionalUsage(positional, context));

            return span;
        }

        public HelpNode CreatePositionalUsage(Positional positional, IContext context)
        {
            var span = new SpanNode();
            span.AddChild(new TextNode("["));
            span.AddChild(CreateUsageAlias(positional, context));
            span.AddChild(new TextNode("]"));
            return span;
        }

        public HelpNode CreateSubCommandUsage(string parserId, IContext context)
        {
            var subCommands = context.HierarchyRepository.GetChildren(parserId).ToList();
            var code = subCommands.Select(x => new CodeNode(x));
            var span = new SpanNode
            {
                Children =
                {
                    new TextNode("[")
                }
            };

            var inner = code.Aggregate(new List<HelpNode>(), (list, codeNode) =>
            {
                if (list.Any()) list.Add(new TextNode("|"));
                list.Add(codeNode);
                return list;
            });

            foreach (var helpNode in inner) span.AddChild(helpNode);

            span.AddChild(new TextNode("]"));
            return span;
        }

        public HelpNode CreateSwitchUsage(Switch @switch, IContext context)
        {
            var span = new SpanNode();
            span.AddChild(new TextNode("["));

            if (@switch.Letter.HasValue && !@switch.Word.IsNullOrWhiteSpace())
            {
                span.AddChild(new CodeNode($"-{@switch.Letter}"));
                span.AddChild(new TextNode(", "));
                span.AddChild(new CodeNode($"--{@switch.Word}"));
            }

            else if (@switch.Letter.HasValue)
            {
                span.AddChild(new CodeNode($"-{@switch.Letter}"));
            }

            else if (!@switch.Word.IsNullOrWhiteSpace())
            {
                span.AddChild(new CodeNode($"--{@switch.Word}"));
            }

            if (@switch.MinRequired != 1)
            {
                span.AddChild(new TextNode(" "));
                span.AddChild(CreateUsageAlias(@switch, context));
            }
            span.AddChild(new TextNode("]"));
            return span;
        }

        public HelpNode CreateUsageAlias(Parameter parameter, IContext context)
        {
            var prefix = "v";
            if (parameter is Positional) prefix = "p";
            var hi = parameter.MaxAllowed == int.MaxValue ? "N" : parameter.MinRequired.ToString();
            if (parameter.MinRequired == parameter.MaxAllowed)
                return new CodeNode($"{prefix}1");
            return new SpanNode
            {
                Children =
                {
                    new CodeNode($"{prefix}1"),
                    new TextNode(".."),
                    new CodeNode($"{prefix}{hi}")
                }
            };
        }
    }
}