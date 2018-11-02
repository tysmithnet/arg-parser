using System;
using ArgParser.Core;
using ArgParser.Core.Validation;

namespace ArgParser.Flavors.Git
{
    public class ParserBuilder : ISubBuilder
    {
        /// <inheritdoc />
        public ParserBuilder(string parserName, GitBuilder parent, IGitContext context)
        {
            Name = parserName.ThrowIfArgumentNull(nameof(parserName));
            Parent = parent.ThrowIfArgumentNull(nameof(parent));
            Context = context.ThrowIfArgumentNull(nameof(context));
        }

        public GitBuilder Parent { get; set; }

        public ParserBuilder WithBooleanCommand(char letter, string word, Action<object> consumeCallback)
        {
            Context.ParameterRepository.AddParameter(Name, new BooleanSwitch(letter, word, consumeCallback));
            return this;
        }

        public ParserBuilder WithFactoryFunctions(params Func<object>[] factoryFunctions)
        {
            if (factoryFunctions == null)
                throw new ArgumentNullException(nameof(factoryFunctions));

            foreach (var factoryFunction in factoryFunctions)
                Context.FactoryFunctionRepository.AddFactoryFunction(Name, factoryFunction);
            return this;
        }

        public ParserBuilder WithPositional(Action<object, string[]> consumeCallback)
        {
            Context.ParameterRepository.AddParameter(Name, new Positional(consumeCallback));
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

        /// <inheritdoc />
        public GitBuilder Build()
        {
            return Parent;
        }
    }
}