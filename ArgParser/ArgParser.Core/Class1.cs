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

    public class Parser<T, TBase> : IParser<T, TBase>, ISwitchContainer<T> where T : TBase
    {
        private Parser<T> ParserInternal { get; } = new Parser<T>();

        /// <inheritdoc />
        public bool CanHandle<TSub>(TSub instance, IIterationInfo info) where TSub : T
        {
            return ParserInternal.CanHandle(instance, info) || BaseParser.CanHandle(instance, info);
        }

        /// <inheritdoc />
        public IIterationInfo Handle<TSub>(TSub instance, IIterationInfo info) where TSub : T
        {
            if (ParserInternal.CanHandle(instance, info))
                return ParserInternal.Handle(instance, info);
            return BaseParser?.Handle(instance, info) ?? info;
        }

        /// <inheritdoc />
        public IParser<TBase> BaseParser { get; set; }

        /// <inheritdoc />
        public void AddSwitch(ISwitch<T> svitch)
        {
            ParserInternal.AddSwitch(svitch);
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

    public class Parser<T> : IParser<T>, ISwitchContainer<T>
    {
        private IList<ISwitch<T>> _switches = new List<ISwitch<T>>();

        public void AddSwitch(ISwitch<T> svitch)
        {
            _switches.Add(svitch);
        }

        /// <inheritdoc />
        public bool CanHandle<TSub>(TSub instance, IIterationInfo info) where TSub : T
        {
            if (_switches.Any(s => s.CanHandle?.Invoke(instance, info) ?? false))
            {
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public IIterationInfo Handle<TSub>(TSub instance, IIterationInfo info) where TSub : T
        {
            var svitch = _switches.FirstOrDefault(s => s.CanHandle?.Invoke(instance, info) ?? false);
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

    public class Lexer : ILexer
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
            var parser = new Parser<BaseOptions>();
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

            var childParser = new Parser<ChildOptions, BaseOptions>();
            childParser.BaseParser = parser;
            
        }
    }
}
