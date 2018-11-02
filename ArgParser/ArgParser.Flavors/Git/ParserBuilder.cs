using System;
using System.Linq;
using ArgParser.Core;
using ArgParser.Core.Validation;

namespace ArgParser.Flavors.Git
{
    public class ParserBuilder<T> : ParserBuilder
    {
        public ParserBuilder(string parserName, GitBuilder parent, IGitContext context) : base(parserName, parent,
            context)
        {
        }

        public ParserBuilder<T> WithBooleanSwitch(char letter, string word, Action<T> consumeCallback)
        {
            Context.ParameterRepository.AddParameter(Name, new BooleanSwitch<T>(letter, word, consumeCallback));
            return this;
        }

        public ParserBuilder<T> WithFactoryFunctions(params Func<T>[] factoryFuncs)
        {
            factoryFuncs.ThrowIfArgumentNull(nameof(factoryFuncs));
            foreach (var factoryFunc in factoryFuncs)
                Context.FactoryFunctionRepository.AddFactoryFunction(Name, factoryFunc.ToBaseFactoryFunction());
            return this;
        }

        public ParserBuilder<T> WithPositional(Action<T, string> consumeCallback)
        {
            void Convert(T instance, string[] strings)
            {
                consumeCallback(instance, strings.First());
            }

            return WithPositionals(Convert, 1, 1);
        }

        public ParserBuilder<T> WithPositionals(Action<T, string[]> consumeCallback, int min = 1,
            int max = int.MaxValue)
        {
            Context.ParameterRepository.AddParameter(Name, new Positional<T>(consumeCallback)
            {
                Min = min,
                Max = max
            });
            return this;
        }

        public ParserBuilder<T> WithSingleValueSwitch(char letter, string word, Action<T, string> consumeCallback)
        {
            Context.ParameterRepository.AddParameter(Name, new SingleValueSwitch<T>(letter, word, consumeCallback));
            return this;
        }

        public ParserBuilder<T> WithValuesSwitch(char letter, string word, Action<T, string[]> consumeCallback)
        {
            Context.ParameterRepository.AddParameter(Name, new ValuesSwitch<T>(letter, word, consumeCallback));
            return this;
        }
    }

    public class ParserBuilder : ISubBuilder
    {
        public ParserBuilder(string parserName, GitBuilder parent, IGitContext context)
        {
            Name = parserName.ThrowIfArgumentNull(nameof(parserName));
            Parent = parent.ThrowIfArgumentNull(nameof(parent));
            Context = context.ThrowIfArgumentNull(nameof(context));
        }

        public GitBuilder Build() => Parent;

        public ParserBuilder WithBooleanSwitch(char letter, string word, Action<object> consumeCallback)
        {
            Context.ParameterRepository.AddParameter(Name, new BooleanSwitch(letter, word, consumeCallback));
            return this;
        }

        public ParserBuilder WithFactoryFunctions(params Func<object>[] factoryFunctions)
        {
            factoryFunctions.ThrowIfArgumentNull(nameof(factoryFunctions));

            foreach (var factoryFunction in factoryFunctions)
                Context.FactoryFunctionRepository.AddFactoryFunction(Name, factoryFunction);
            return this;
        }

        public ParserBuilder WithPositional(Action<object, string> consumeCallback)
        {
            void Convert(object instance, string[] strings)
            {
                consumeCallback(instance, strings.First());
            }

            return WithPositionals(Convert, 1, 1);
        }

        public ParserBuilder WithPositionals(Action<object, string[]> consumeCallback, int min = 1,
            int max = int.MaxValue)
        {
            Context.ParameterRepository.AddParameter(Name, new Positional(consumeCallback)
            {
                Min = min,
                Max = max
            });
            return this;
        }

        public ParserBuilder WithSingleValueSwitch(char letter, string word, Action<object, string> consumeCallback)
        {
            Context.ParameterRepository.AddParameter(Name, new SingleValueSwitch(letter, word, consumeCallback));
            return this;
        }

        public ParserBuilder WithValidation(IValidator validator)
        {
            Context.ValidatorRepository.AddValidator(Name, validator);
            return this;
        }

        public ParserBuilder WithValueSwitch(char letter, string word, Action<object, string[]> consumeCallback)
        {
            Context.ParameterRepository.AddParameter(Name, new ValuesSwitch(letter, word, consumeCallback));
            return this;
        }

        public IGitContext Context { get; set; }

        public string Name { get; }

        public GitBuilder Parent { get; set; }
    }
}