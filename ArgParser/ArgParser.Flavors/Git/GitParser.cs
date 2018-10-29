using System;
using ArgParser.Core;
using ArgParser.Core.Help;

namespace ArgParser.Flavors.Git
{
    public class GitParser : IParser
    {
        /// <inheritdoc />
        public GitParser(GitFlavor flavor)
        {
            _flavor = flavor ?? throw new ArgumentNullException(nameof(flavor));
        }

        private readonly GitFlavor _flavor;

        public virtual void AddParameter(IParameter parameter, IGenericHelp help = null)
        {
            DefaultParser.AddParameter(parameter, help);
        }

        /// <inheritdoc />
        public bool CanConsume(object instance, IIterationInfo info)
        {
            if (info.Index == 0 && _flavor.SubCommands.ContainsKey(info.Current.Raw)) return true;
            var canSelf = DefaultParser.CanConsume(instance, info);
            var canBase = _flavor.BaseFlavor?.Parser.CanConsume(instance, info) ?? false;
            return canSelf || canBase;
        }

        /// <inheritdoc />
        public IIterationInfo Consume(object instance, IIterationInfo info)
        {
            if (info.Index == 0 && _flavor.SubCommands.ContainsKey(info.Current.Raw)) return info.Consume(1);
            var canSelf = DefaultParser.CanConsume(instance, info);
            if (canSelf)
                return DefaultParser.Consume(instance, info);
            var canBase = _flavor.BaseFlavor?.Parser.CanConsume(instance, info) ?? false;
            if (canBase)
                return _flavor.BaseFlavor.Parser.Consume(instance, info);
            throw new InvalidOperationException(""); // todo: fix
        }

        /// <inheritdoc />
        public IParser BaseParser
        {
            get => DefaultParser.BaseParser;
            set => DefaultParser.BaseParser = value;
        }

        public DefaultParser DefaultParser { get; set; } = new DefaultParser();

        /// <inheritdoc />
        public IGenericHelp Help => DefaultParser.Help;
    }
}