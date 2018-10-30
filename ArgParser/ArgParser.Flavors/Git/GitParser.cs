using System;
using System.Collections.Generic;
using System.Linq;
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
        private List<IParameter> AllParameters = new List<IParameter>();
        public virtual void AddParameter(IParameter parameter, IGenericHelp help = null)
        {
            AllParameters.Add(parameter);
            DefaultParser.AddParameter(parameter, help);
        }

        /// <inheritdoc />
        public bool CanConsume(object instance, IIterationInfo info)
        {
            if (_isSubCommand && _flavor.SubCommands.ContainsKey(info.Current.Raw)) return true;
            _isSubCommand = false;
            var canSelf = DefaultParser.CanConsume(instance, info);
            var canBase = _flavor.BaseFlavor?.Parser.CanConsume(instance, info) ?? false;
            return canSelf || canBase;
        }

        /// <inheritdoc />
        public IIterationInfo Consume(object instance, IIterationInfo info)
        {
            GitFlavor itr = _flavor;

            while (_isSubCommand)
            {
                if (itr.SubCommands.ContainsKey(info.Current.Raw))
                {
                    itr = itr.SubCommands[info.Current.Raw];
                    info = info.Consume(1);
                }
                else
                {
                    _isSubCommand = false;
                    return info;
                }
            }
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

        private bool _isSubCommand = true;
        /// <inheritdoc />
        public void Reset()
        {
            _isSubCommand = true;
            foreach (var allParameter in AllParameters)
            {
                allParameter.Reset();
            }
        }

        public DefaultParser DefaultParser { get; set; } = new DefaultParser();

        /// <inheritdoc />
        public IGenericHelp Help => DefaultParser.Help;
    }
}