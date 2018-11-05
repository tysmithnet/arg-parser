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

    public class ParameterBuilder
    {
        public ParameterBuilder(ParserBuilder parent, Parameter parameter)
        {
            Finish = parent.ThrowIfArgumentNull(nameof(parent));
            Parameter = parameter.ThrowIfArgumentNull(nameof(parameter));
        }

        public ParserBuilder Finish { get; protected internal set; }
        public Parameter Parameter { get; protected internal set; }
    }
}