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
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public bool CanConsume(object instance, IIterationInfo info)
        {
            var canSelf = DefaultParser.CanConsume(instance, info);
            var canBase = GitContext.GitParserRepository.GetParent(Name)?.CanConsume(instance, info) ?? false;
            return canSelf || canBase;
        }

        public IIterationInfo Consume(object instance, IIterationInfo info)
        {
            var canSelf = DefaultParser.CanConsume(instance, info);
            if (canSelf)
                return DefaultParser.Consume(instance, info);
            var ancestors = GitContext.GitParserRepository.GetAncestors(Name);
            foreach (var gitFlavor in ancestors)
                if (gitFlavor.CanConsume(instance, info))
                    return gitFlavor.Consume(instance, info);
            throw new InvalidOperationException(
                $"Consume was called on {Name}, but it, nor its ancestors are able to consume. Was CanConsume called before this invocation?");
        }

        public IParseResult Parse(string[] args, IEnumerable<Func<object>> factoryFunctions = null)
        {
            return null;
        }

        public void Reset()
        {
            DefaultParser = new DefaultParser();
            var parameters = GitContext.GitParameterRepository.GetParameters(Name).ToList();
            foreach (var parameter in parameters)
            {
                parameter.Reset();
                DefaultParser.AddParameter(parameter);
            }
        }

        public IParser BaseParser => GitContext.GitParserRepository.GetParent(Name);
        public DefaultParser DefaultParser { get; set; } = new DefaultParser();
        public IGitContext GitContext { get; set; }
        public IGenericHelp Help => DefaultParser.Help;
        public string Name { get; set; }
    }
}