using System;
using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class ParserBuilder
    {
        public ParserBuilder(ContextBuilder parent, Parser parser)
        {
            Finish = parent.ThrowIfArgumentNull(nameof(parent));
            Parser = parser.ThrowIfArgumentNull(nameof(parser));
        }

        public ParserBuilder WithBooleanSwitch(char? letter, string word, Action<object> consumeCallback)
        {
            var sw = new BooleanSwitch(letter, word, consumeCallback);
            Parser.AddParameter(sw);
            return this;
        }

        public ParserBuilder WithFactoryFunction(Func<object> func)
        {
            Parser.FactoryFunction = func.ThrowIfArgumentNull(nameof(func));
            return this;
        }

        public ContextBuilder Finish { get; protected internal set; }
        public Parser Parser { get; protected internal set; }
    }

    public class ParserBuilder<T> : ParserBuilder
    {
        public ParserBuilder(ContextBuilder parent, Parser<T> parser) : base(parent, parser)
        {
            Finish = parent.ThrowIfArgumentNull(nameof(parent));
            Parser = parser.ThrowIfArgumentNull(nameof(parser));
        }

        public ParserBuilder WithBooleanSwitch(char? letter, string word, Action<T> consumeCallback)
        {
            var sw = new BooleanSwitch<T>(letter, word, consumeCallback);
            Parser.AddParameter(sw);
            return this;
        }

        public ParserBuilder WithFactoryFunction(Func<T> func)
        {
            Parser.FactoryFunction = func.ThrowIfArgumentNull(nameof(func));
            return this;
        }

        public new Parser<T> Parser
        {
            get => base.Parser as Parser<T>;
            set => base.Parser = value;
        }
    }
}