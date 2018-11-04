using System;
using System.Collections.Generic;
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
            var canSelf = false; // todo: fix
            var canBase = Context.ParserRepository.GetParent(Name)?.CanConsume(instance, info) ?? false;
            return canSelf || canBase;
        }

        public IIterationInfo Consume(object instance, IIterationInfo info)
        {
            var canSelf = CanConsume(instance, info);
            if (canSelf)
                return Consume(instance, info);
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
            var currentParser = Name;
            while (args.Any() && Context.ParserRepository.IsSubCommand(currentParser, args[0]))
            {
                currentParser = Context.ParserRepository.GetSubCommand(currentParser, args[0]).Name;
                args = args.Skip(1).ToArray();
            }

            var ancestors = Context.ParserRepository.GetAncestors(currentParser).ToList();
            ancestors.Insert(0, Context.ParserRepository.Get(currentParser));
            var funcs = ancestors
                .Where(x => Context.FactoryFunctionRepository.Contains(x.Name))
                .SelectMany(x => Context.FactoryFunctionRepository.GetFactoryFunctions(x.Name));
            var strat = new GitParseStrategy(Context);
            return strat.Parse(ancestors, args);
        }

        public void Reset()
        {
            if (!Context.ParameterRepository.Contains(Name))
                return;
            var parameters = Context.ParameterRepository.GetParameters(Name).ToList();
            foreach (var parameter in parameters)
            {
                parameter.Reset();
                AddParameter(parameter);
            }
        }

        public IParser BaseParser => Context.ParserRepository.GetParent(Name);
        public IGitContext Context { get; set; }
        public IGenericHelp Help { get; set; }
        public string Name { get; set; }
        public DefaultHelpBuilder HelpBuilder { get; set; } = new DefaultHelpBuilder();
        protected internal List<GitParameter> Parameters { get; set; } = new List<GitParameter>();

        public virtual void AddHelp(IGenericHelp help)
        {
            Help = help;
            HelpBuilder.AddGenericHelp(help);
        }

        public virtual void AddParameter(IParameter parameter, IGenericHelp help = null)
        {
            if (!(parameter is GitParameter casted))
                return;
            Parameters.Add(casted);
            if (help == null)
                return;
            HelpBuilder.AddParameter(help.Name,
                help.Examples?.SelectMany(e => e?.Usage).Where(x => !x.IsNullOrWhiteSpace()).ToArray(),
                help.ShortDescription);
        }
    }
}