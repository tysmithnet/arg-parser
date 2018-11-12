using System;
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

        public ParserBuilder WithBooleanSwitch(char? letter, string word, Action<object> consumeCallback,
            Action<ParameterHelpBuilder> helpSetupCallback = null, bool required = false)
        {
            var sw = new BooleanSwitch(Parser, letter, word, consumeCallback)
            {
                IsRequired = required
            };
            AddParameterInternal(sw, helpSetupCallback);
            return this;
        }

        public ParserBuilder WithFactoryFunction(Func<object> func)
        {
            Parser.FactoryFunction = func.ThrowIfArgumentNull(nameof(func));
            return this;
        }

        public ParserBuilder WithPositional(Action<object, string> consumeCallback,
            Action<ParameterHelpBuilder> helpSetupCallback = null, bool required = false)
        {
            var sw = new Positional(Parser, (o, strings) => consumeCallback(o, strings.Single()), 1, 1)
            {
                IsRequired = required
            };
            AddParameterInternal(sw, helpSetupCallback);
            return this;
        }

        public ParserBuilder WithPositionals(Action<object, string[]> consumeCallback, int min = 1,
            int max = int.MaxValue, Action<ParameterHelpBuilder> helpSetupCallback = null, bool required = false)
        {
            var sw = new Positional(Parser, consumeCallback, min, max)
            {
                IsRequired = required
            };
            AddParameterInternal(sw, helpSetupCallback);
            return this;
        }

        public ParserBuilder WithSingleValueSwitch(char? letter, string word, Action<object, string> consumeCallback,
            Action<ParameterHelpBuilder> helpSetupCallback = null, bool required = false)
        {
            var sw = new SingleValueSwitch(Parser, letter, word, consumeCallback)
            {
                MinRequired = 1,
                MaxAllowed = 1,
                IsRequired = required
            };
            AddParameterInternal(sw, helpSetupCallback);
            return this;
        }

        public ParserBuilder WithValuesSwitch(char? letter, string word, Action<object, string[]> consumeCallback,
            int min = 1, int max = int.MaxValue, Action<ParameterHelpBuilder> helpSetupCallback = null, bool required = false)
        {
            var sw = new ValuesSwitch(Parser, letter, word, consumeCallback)
            {
                MinRequired = min,
                MaxAllowed = max,
                IsRequired = required
            };
            AddParameterInternal(sw, helpSetupCallback);
            return this;
        }

        protected void AddParameterInternal(Parameter parameter, Action<ParameterHelpBuilder> helpSetupCallback = null)
        {
            Parser.AddParameter(parameter);
            if (helpSetupCallback != null)
            {
                var builder = new ParameterHelpBuilder(parameter);
                helpSetupCallback(builder);
                parameter.Help = builder.Build();
            }
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

        public ParserBuilder<T> WithBooleanSwitch(char? letter, string word, Action<T> consumeCallback,
            Action<ParameterHelpBuilder> helpSetupCallback = null, bool required = false)
        {
            var sw = new BooleanSwitch<T>(Parser, letter, word, consumeCallback)
            {
                IsRequired = required
            };
            AddParameterInternal(sw, helpSetupCallback);
            return this;
        }

        public ParserBuilder<T> WithFactoryFunction(Func<T> func)
        {
            Parser.FactoryFunction = func.ThrowIfArgumentNull(nameof(func));
            return this;
        }

        public ParserBuilder<T> WithPositional(Action<T, string> consumeCallback,
            Action<ParameterHelpBuilder> helpSetupCallback = null, bool required = false)
        {
            var sw = new Positional<T>(Parser, (o, strings) => consumeCallback(o, strings.Single()), 1, 1)
            {
                IsRequired = required
            };
            AddParameterInternal(sw, helpSetupCallback);
            return this;
        }

        public ParserBuilder<T> WithPositionals(Action<T, string[]> consumeCallback, int min = 1,
            int max = int.MaxValue, Action<ParameterHelpBuilder> helpSetupCallback = null, bool required = false)
        {
            var sw = new Positional<T>(Parser, consumeCallback, min, max)
            {
                IsRequired = required
            };
            AddParameterInternal(sw, helpSetupCallback);
            return this;
        }

        public ParserBuilder<T> WithSingleValueSwitch(char? letter, string word, Action<T, string> consumeCallback,
            Action<ParameterHelpBuilder> helpSetupCallback = null, bool required = false)
        {
            var sw = new SingleValueSwitch<T>(Parser, letter, word, consumeCallback)
            {
                IsRequired = required
            };
            AddParameterInternal(sw, helpSetupCallback);
            return this;
        }

        public ParserBuilder<T> WithValuesSwitch(char? letter, string word, Action<T, string[]> consumeCallback,
            int min = 1, int max = int.MaxValue, Action<ParameterHelpBuilder> helpSetupCallback = null, bool required = false)
        {
            var sw = new ValuesSwitch<T>(Parser, letter, word, consumeCallback)
            {
                IsRequired = required
            };
            AddParameterInternal(sw, helpSetupCallback);
            return this;
        }

        public new Parser<T> Parser
        {
            get => base.Parser as Parser<T>;
            set => base.Parser = value;
        }
    }
}