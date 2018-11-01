using System;
using System.Collections.Generic;
using System.Composition;
using System.Diagnostics;
using ArgParser.Core;
using ArgParser.Core.Help;

namespace ArgParser.Flavors.Git
{
    [DebuggerDisplay("{Name}")]
    public class GitParser : IParser
    {
        public GitParser(string flavorName)
        {
            Name = flavorName ?? throw new ArgumentNullException(nameof(flavorName));
        }

        private readonly List<IParameter> _allParameters = new List<IParameter>();

        public virtual void AddParameter(IParameter parameter, IGenericHelp help = null)
        {
            _allParameters.Add(parameter);
            DefaultParser.AddParameter(parameter, help);
        }

        public bool CanConsume(object instance, IIterationInfo info)
        {
            var canSelf = DefaultParser.CanConsume(instance, info);
            var canBase = GitFlavorRepository.GetParent(Name)?.CanConsume(instance, info) ?? false;
            return canSelf || canBase;
        }

        public IIterationInfo Consume(object instance, IIterationInfo info)
        {
            var canSelf = DefaultParser.CanConsume(instance, info);
            if (canSelf)
                return DefaultParser.Consume(instance, info);
            var ancestors = GitFlavorRepository.GetAncestors(Name);
            foreach (var gitFlavor in ancestors)
                if (gitFlavor.CanConsume(instance, info))
                    return gitFlavor.Consume(instance, info);
            throw new InvalidOperationException($"Consume was called on {Name}, but it, nor its ancestors are able to consume. Was CanConsume called before this invocation?");
        }

        public void Reset()
        {
            foreach (var allParameter in _allParameters) allParameter.Reset();
        }

        public IParser BaseParser => GitFlavorRepository.GetParent(Name)?.Parser;
        public DefaultParser DefaultParser { get; set; } = new DefaultParser();

        [Import]
        public IGitFlavorRepository GitFlavorRepository { get; set; }

        public IGenericHelp Help => DefaultParser.Help;
        public string Name { get; set; }
    }
}