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
        /// <inheritdoc />
        public GitParser(GitFlavor flavor)
        {
            _flavor = flavor ?? throw new ArgumentNullException(nameof(flavor));
        }

        private readonly GitFlavor _flavor;
        private readonly List<IParameter> AllParameters = new List<IParameter>();

        public virtual void AddParameter(IParameter parameter, IGenericHelp help = null)
        {
            AllParameters.Add(parameter);
            DefaultParser.AddParameter(parameter, help);
        }

        /// <inheritdoc />
        public bool CanConsume(object instance, IIterationInfo info)
        {
            var canSelf = DefaultParser.CanConsume(instance, info);
            var canBase = _flavor.BaseFlavor?.Parser.CanConsume(instance, info) ?? false;
            return canSelf || canBase;
        }

        /// <inheritdoc />
        public IIterationInfo Consume(object instance, IIterationInfo info)
        {
            var canSelf = DefaultParser.CanConsume(instance, info);
            if (canSelf)
                return DefaultParser.Consume(instance, info);
            var canBase = _flavor.BaseFlavor?.Parser.CanConsume(instance, info) ?? false;
            if (canBase)
                return _flavor.BaseFlavor.Parser.Consume(instance, info);
            throw new InvalidOperationException(""); // todo: fix
        }

        /// <inheritdoc />
        public void Reset()
        {
            foreach (var allParameter in AllParameters) allParameter.Reset();
        }

        /// <inheritdoc />
        public IParser BaseParser => DefaultParser.BaseParser;

        public DefaultParser DefaultParser { get; set; } = new DefaultParser();

        /// <inheritdoc />
        public IGenericHelp Help => DefaultParser.Help;

        public string Name => _flavor.Name;
    }
}