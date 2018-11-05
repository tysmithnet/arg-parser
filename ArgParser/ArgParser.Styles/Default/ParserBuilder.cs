﻿using System;
using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class ParserBuilder
    {
        public ParserBuilder(ContextBuilder parent, Parser parser)
        {
            Parent = parent.ThrowIfArgumentNull(nameof(parent));
            Parser = parser.ThrowIfArgumentNull(nameof(parser));
        }

        public ContextBuilder Build() => Parent;

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

        public ContextBuilder Parent { get; protected internal set; }
        public Parser Parser { get; protected internal set; }
    }
}