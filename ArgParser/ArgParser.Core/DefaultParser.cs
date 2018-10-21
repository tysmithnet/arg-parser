using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public class DefaultParser<T, TBase> : IParser<T, TBase>, ISwitchContainer<T> where T : TBase
    {
        private DefaultParser<T> DefaultParserInternal { get; } = new DefaultParser<T>();

        /// <inheritdoc />
        public bool CanHandle<TSub>(TSub instance, IIterationInfo info) where TSub : T
        {
            return DefaultParserInternal.CanHandle(instance, info) || BaseParser.CanHandle(instance, info);
        }

        /// <inheritdoc />
        public IIterationInfo Handle<TSub>(TSub instance, IIterationInfo info) where TSub : T
        {
            if (DefaultParserInternal.CanHandle(instance, info))
                return DefaultParserInternal.Handle(instance, info);
            return BaseParser?.Handle(instance, info) ?? info;
        }

        /// <inheritdoc />
        public IParser<TBase> BaseParser { get; set; }

        /// <inheritdoc />
        public void AddSwitch(ISwitch<T> svitch)
        {
            DefaultParserInternal.AddSwitch(svitch);
        }
    }

    public class DefaultParser<T> : IParser<T>, ISwitchContainer<T>
    {
        protected internal IList<ISwitch<T>> Switches { get; set; } = new List<ISwitch<T>>();
        protected internal ILexer Lexer { get; set; } = new DefaultLexer();

        public void AddSwitch(ISwitch<T> svitch)
        {
            Switches.Add(svitch);
        }

        /// <inheritdoc />
        public virtual bool CanHandle<TSub>(TSub instance, IIterationInfo info) where TSub : T
        {
            if (Switches.Any(s => s.CanHandle?.Invoke(instance, info) ?? false))
            {
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public virtual IIterationInfo Handle<TSub>(TSub instance, IIterationInfo info) where TSub : T
        {
            var svitch = Switches.FirstOrDefault(s => s.CanHandle?.Invoke(instance, info) ?? false);
            return svitch?.Handle?.Invoke(instance, info) ?? info;
        }
    }
}