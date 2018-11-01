using System;
using System.Collections.Generic;
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
            var name = flavorName ?? throw new ArgumentNullException(nameof(flavorName));
            _flavor = GitFlavorRepository.Get(name);
        }

        private readonly List<IParameter> _allParameters = new List<IParameter>();
        private readonly GitFlavor _flavor;

        public virtual void AddParameter(IParameter parameter, IGenericHelp help = null)
        {
            _allParameters.Add(parameter);
            DefaultParser.AddParameter(parameter, help);
        }

        public bool CanConsume(object instance, IIterationInfo info)
        {
            var canSelf = DefaultParser.CanConsume(instance, info);
            var canBase = _flavor.BaseFlavor?.Parser.CanConsume(instance, info) ?? false;
            return canSelf || canBase;
        }

        public IIterationInfo Consume(object instance, IIterationInfo info)
        {
            var canSelf = DefaultParser.CanConsume(instance, info);
            if (canSelf)
                return DefaultParser.Consume(instance, info);
            var ancestors = GitFlavorRepository.GetAncestors(_flavor.Name);
            foreach (var gitFlavor in ancestors)
                if (gitFlavor.CanConsume(instance, info))
                    return gitFlavor.Consume(instance, info);
            throw new InvalidOperationException(""); // todo: fix
        }

        public void Reset()
        {
            foreach (var allParameter in _allParameters) allParameter.Reset();
        }

        public IParser BaseParser => GitFlavorRepository.GetParent(_flavor.Name)?.Parser;
        public DefaultParser DefaultParser { get; set; } = new DefaultParser();
        public IGitFlavorRepository GitFlavorRepository { get; set; }
        public IGenericHelp Help => DefaultParser.Help;
        public string Name => $"{_flavor.Name}_parser";
    }
}