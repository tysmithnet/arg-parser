﻿using System;
using System.Linq;
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

        public ParserBuilder WithSingleValueSwitch(char? letter, string word, Action<object, string> consumeCallback)
        {
            var sw = new SingleValueSwitch(letter, word, consumeCallback);
            Parser.AddParameter(sw);
            return this;
        }

        public ParserBuilder WithValuesSwitch(char? letter, string word, Action<object, string[]> consumeCallback)
        {
            var sw = new ValuesSwitch(letter, word, consumeCallback);
            Parser.AddParameter(sw);
            return this;
        }

        public ParserBuilder WithPositional(Action<object, string> consumeCallback)
        {
            var sw = new Positional((o, strings) => consumeCallback(o, strings.Single()), 1, 1);
            Parser.AddParameter(sw);
            return this;
        }

        public ParserBuilder WithPositionals(Action<object, string[]> consumeCallback, int min = 1, int max = int.MaxValue)
        {
            var sw = new Positional(consumeCallback, min, max);
            Parser.AddParameter(sw);
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

        public ParserBuilder<T> WithBooleanSwitch(char? letter, string word, Action<T> consumeCallback)
        {
            var sw = new BooleanSwitch<T>(letter, word, consumeCallback);
            Parser.AddParameter(sw);
            return this;
        }

        public ParserBuilder<T> WithFactoryFunction(Func<T> func)
        {
            Parser.FactoryFunction = func.ThrowIfArgumentNull(nameof(func));
            return this;
        }

        public ParserBuilder<T> WithSingleValueSwitch(char? letter, string word, Action<T, string> consumeCallback)
        {
            var sw = new SingleValueSwitch<T>(letter, word, consumeCallback);
            Parser.AddParameter(sw);
            return this;
        }

        public ParserBuilder<T> WithValuesSwitch(char? letter, string word, Action<T, string[]> consumeCallback)
        {
            var sw = new ValuesSwitch<T>(letter, word, consumeCallback);
            Parser.AddParameter(sw);
            return this;
        }

        public ParserBuilder<T> WithPositional(Action<T, string> consumeCallback)
        {
            var sw = new Positional<T>((o, strings) => consumeCallback(o, strings.Single()), 1, 1);
            Parser.AddParameter(sw);
            return this;
        }

        public ParserBuilder<T> WithPositionals(Action<T, string[]> consumeCallback, int min = 1, int max = int.MaxValue)
        {
            var sw = new Positional<T>(consumeCallback, min, max);
            Parser.AddParameter(sw);
            return this;
        }

        public new Parser<T> Parser
        {
            get => base.Parser as Parser<T>;
            set => base.Parser = value;
        }
    }
}