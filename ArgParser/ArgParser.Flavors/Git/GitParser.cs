using System;
using System.Diagnostics;
using System.Linq;
using ArgParser.Core;
using ArgParser.Core.Help;

namespace ArgParser.Flavors.Git
{
    [DebuggerDisplay("{Name}")]
    public class GitParser : IParser
    {
        public GitParser(string name)
        {
            Name = name.ThrowIfArgumentNull(nameof(name));
        }

        public bool CanConsume(object instance, IIterationInfo info)
        {
            var canSelf = DefaultParser.CanConsume(instance, info);
            var canBase = Context.ParserRepository.GetParent(Name)?.CanConsume(instance, info) ?? false;
            return canSelf || canBase;
        }

        public IIterationInfo Consume(object instance, IIterationInfo info)
        {
            var canSelf = DefaultParser.CanConsume(instance, info);
            if (canSelf)
                return DefaultParser.Consume(instance, info);
            var ancestors = Context.ParserRepository.GetAncestors(Name);
            foreach (var gitFlavor in ancestors)
                if (gitFlavor.CanConsume(instance, info))
                    return gitFlavor.Consume(instance, info);
            throw new InvalidOperationException(
                $"Consume was called on {Name}, but it, nor its ancestors are able to consume. Was CanConsume called before this invocation?");
        }

        public IParseResult Parse(string[] args)
        {
            args.ThrowIfArgumentNull(nameof(args));
            string currentParser = Name;
            while (args.Any() && Context.ParserRepository.IsSubCommand(currentParser, args[0]))
            {
                currentParser = Context.ParserRepository.GetSubCommand(currentParser, args[0]).Name;
                args = args.Skip(1).ToArray();
            }

            var ancestors = Context.ParserRepository.GetAncestors(currentParser).ToList();
            ancestors.Insert(0, this);
            var funcs = ancestors.SelectMany(x => Context.FactoryFunctionRepository.GetFactoryFunctions(x.Name));
            var strat = new GitParseStrategy(Context);
            return strat.Parse(ancestors, funcs, args);
        }

        public void Reset()
        {
            DefaultParser = new DefaultParser();
            var parameters = Context.ParameterRepository.GetParameters(Name).ToList();
            foreach (var parameter in parameters)
            {
                parameter.Reset();
                DefaultParser.AddParameter(parameter);
            }
        }

        public IParser BaseParser => Context.ParserRepository.GetParent(Name);
        public DefaultParser DefaultParser { get; set; } = new DefaultParser();
        public IGitContext Context { get; set; }
        public IGenericHelp Help => DefaultParser.Help;
        public string Name { get; set; }
    }
}