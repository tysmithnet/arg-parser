using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public interface IToken
    {
        string Raw { get; }
    }

    public interface IIterationInfo
    {
        IToken Current { get; }
        IToken Next { get; }
        IReadOnlyList<IToken> Tokens { get; }
        int Index { get; }
        string[] Args { get; }
        IIterationInfo Consume(int numTokens);
    }

    public class IterationInfo : IIterationInfo
    {
        /// <inheritdoc />
        public IToken Current => Tokens?[Index];

        /// <inheritdoc />
        public IToken Next => Tokens.Count < Index + 1 ? Tokens[Index + 1] : null;

        /// <inheritdoc />
        public IReadOnlyList<IToken> Tokens { get; internal set; }

        /// <inheritdoc />
        public int Index { get; set; }

        public IIterationInfo SetTokens(IList<IToken> tokens)
        {
            return Clone(info => info.Tokens = tokens?.ToList());
        }

        private IterationInfo Clone(Action<IterationInfo> transformer)
        {
            var newGuy = new IterationInfo()
            {
                Args = Args?.ToArray(),
                Tokens = Tokens?.ToList(),
                Index = Index,
            };
            transformer?.Invoke(newGuy);
            return newGuy;
        }

        public IIterationInfo SetIndex(int i)
        {
            return Clone(info => info.Index = i);
        }

        /// <inheritdoc />
        public string[] Args { get; set; }

        /// <inheritdoc />
        public IIterationInfo Consume(int numTokens)
        {
            return SetIndex(Index + numTokens);
        }
    }

    public interface IParser<in T, in TBase> : IParser<T> where T : TBase
    {
        IParser<TBase> BaseParser { get; }
    }

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

    public interface IIterationInfoFactory
    {
        IIterationInfo Create(string[] args);
    }

    public class DefaultIterationInfoFactory : IIterationInfoFactory
    {
        protected internal ILexer Lexer { get; set; } = new DefaultLexer();

        /// <inheritdoc />
        public IIterationInfo Create(string[] args)
        {
            var tokens = Lexer.Lex(args);
            return new IterationInfo()
            {
                Tokens = tokens.ToList(),
                Index = 0,
                Args = args.ToArray()
            };
        }
    }

    public interface IOptionsBuilder<out T>
    {
        T Build(string[] args);
    }

    public class DefaultOptionsBuilder<T> : IOptionsBuilder<T>
    {
        protected internal IIterationInfoFactory IterationInfoFactory { get; set; } = new DefaultIterationInfoFactory();
        protected internal Func<T> FactoryFucntion { get; set; }
        protected internal IParser<T> Parser { get; set; } = new DefaultParser<T>();

        /// <inheritdoc />
        public DefaultOptionsBuilder(Func<T> factoryFucntion)
        {
            FactoryFucntion = factoryFucntion ?? throw new ArgumentNullException(nameof(factoryFucntion));
        }

        /// <inheritdoc />
        public T Build(string[] args)
        {
            var info = IterationInfoFactory.Create(args);
            var instance = FactoryFucntion();
            if (Parser.CanHandle(instance, info))
            {
                Parser.Handle(instance, new IterationInfo());
                return instance;
            }
            return default(T);
        }
    }

    public interface IParser<in T>
    {
        bool CanHandle<TSub>(TSub instance, IIterationInfo info) where TSub : T;
        IIterationInfo Handle<TSub>(TSub instance, IIterationInfo info) where TSub : T;
    }

    public interface ISwitchContainer<out T>
    {
        void AddSwitch(ISwitch<T> svitch);
    }

    public class BaseOptions
    {
        public bool IsThing { get; set; }
        public string Thing { get; set; }
    }

    public class ChildOptions : BaseOptions
    {
        public string Number { get; set; }
    }

    public interface ISwitch<in T>
    {
        CanHandleCallback<T> CanHandle { get; }
        HandlerCallback<T> Handle { get; }
    }

    public delegate bool CanHandleCallback<in T>(T instance, IIterationInfo info);

    public delegate IIterationInfo HandlerCallback<in T>(T instance, IIterationInfo info);

    public class Switch<T> : ISwitch<T>
    {
        /// <inheritdoc />
        public CanHandleCallback<T> CanHandle { get; set; }

        /// <inheritdoc />
        public HandlerCallback<T> Handle { get; set; }
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

    public interface ILexer
    {
        IEnumerable<IToken> Lex(string[] args);
    }

    public class Token : IToken
    {
        /// <inheritdoc />
        public Token(string raw)
        {
            Raw = raw ?? throw new ArgumentNullException(nameof(raw));
        }

        /// <inheritdoc />
        public string Raw { get; }
    }

    public class DefaultLexer : ILexer
    {
        /// <inheritdoc />
        public IEnumerable<IToken> Lex(string[] args)
        {
            return args?.Select(a => new Token(a)).ToList() ?? new List<Token>();
        }
    }

    public class Doer
    {
        public void DoStuff(string[] args)
        {
            var parser = new DefaultParser<BaseOptions>();
            parser.AddSwitch(new Switch<BaseOptions>()
            {
                CanHandle = (instance, info) => info.Current.Raw == "do",
                Handle = (instance, info) =>
                {
                    instance.IsThing = true;
                    return info.Consume(1);
                }
            });
            parser.AddSwitch(new Switch<BaseOptions>()
            {
                CanHandle = (instance, info) => info.Current.Raw == "-t",
                Handle = (instance, info) =>
                {
                    instance.Thing = info.Next?.Raw ?? "unknown";
                    return info.Consume(2);
                }
            });

            var childParser = new DefaultParser<ChildOptions, BaseOptions>();
            childParser.BaseParser = parser;
            
        }
    }
}
