using System;
using System.Collections.Generic;
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

        public ParserBuilder<T> WithBooleanSwitch(char? letter, string word, Action<T> consumeCallback,
            bool isRequired = false)
        {
            var booleanSwitch = new BooleanSwitch<T>(letter, word, consumeCallback);
            Context.ParameterRepository.AddParameter(Name, booleanSwitch);
            if (isRequired)
                Context.ValidatorRepository.AddValidator(Name, new RequiredParameterValidator(booleanSwitch));
            return this;
        }

        public ParserBuilder<T> WithFactoryFunctions(params Func<T>[] factoryFuncs)
        {
            factoryFuncs.ThrowIfArgumentNull(nameof(factoryFuncs));
            foreach (var factoryFunc in factoryFuncs)
                Context.FactoryFunctionRepository.AddFactoryFunction(Name, factoryFunc.ToBaseFactoryFunction());
            return this;
        }

        public ParserBuilder<T> WithPositional(Action<T, string> consumeCallback, bool isRequired = false,
            Func<string, IEnumerable<ParseError>> isValid = null)
        {
            void Convert(T instance, string[] strings)
            {
                consumeCallback(instance, strings.First());
            }

            return WithPositionals(Convert, 1, 1, isRequired);
        }

        public ParserBuilder<T> WithPositionals(Action<T, string[]> consumeCallback, int min = 1,
            int max = int.MaxValue, bool isRequired = false, Func<string, IEnumerable<ParseError>> isValid = null)
        {
            var newGuy = new Positional<T>(consumeCallback)
            {
                Min = min,
                Max = max
            };
            Context.ParameterRepository.AddParameter(Name, newGuy);
            if (isRequired)
                Context.ValidatorRepository.AddValidator(Name, new RequiredParameterValidator(newGuy));
            return this;
        }

        public ParserBuilder<T> WithSingleValueSwitch(char? letter, string word, Action<T, string> consumeCallback,
            bool isRequired = false, Func<string, IEnumerable<ParseError>> isValid = null)
        {
            var singleValueSwitch = new SingleValueSwitch<T>(letter, word, consumeCallback);
            Context.ParameterRepository.AddParameter(Name, singleValueSwitch);
            if (isRequired)
                Context.ValidatorRepository.AddValidator(Name, new RequiredParameterValidator(singleValueSwitch));
            return this;
        }

        public ParserBuilder<T> WithValuesSwitch(char? letter, string word, Action<T, string[]> consumeCallback,
            bool isRequired = false, Func<string, IEnumerable<ParseError>> isValid = null)
        {
            var valuesSwitch = new ValuesSwitch<T>(letter, word, consumeCallback);
            Context.ParameterRepository.AddParameter(Name, valuesSwitch);
            if (isRequired)
                Context.ValidatorRepository.AddValidator(Name, new RequiredParameterValidator(valuesSwitch));
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

        public ParserBuilder WithBooleanSwitch(char? letter, string word, Action<object> consumeCallback,
            bool isRequired = false)
        {
            var booleanSwitch = new BooleanSwitch(letter, word, consumeCallback);
            Context.ParameterRepository.AddParameter(Name, booleanSwitch);
            if (isRequired)
                Context.ValidatorRepository.AddValidator(Name,
                    new RequiredParameterValidator(booleanSwitch)); // todo: lot of duplicate code here
            return this;
        }

        public ParserBuilder WithFactoryFunctions(params Func<object>[] factoryFunctions)
        {
            factoryFunctions.ThrowIfArgumentNull(nameof(factoryFunctions));

            foreach (var factoryFunction in factoryFunctions)
                Context.FactoryFunctionRepository.AddFactoryFunction(Name, factoryFunction);
            return this;
        }

        public ParserBuilder WithPositional(Action<object, string> consumeCallback, bool isRequired = false,
            Func<string, IEnumerable<ParseError>> isValid = null)
        {
            void Convert(object instance, string[] strings)
            {
                consumeCallback(instance, strings.First());
            }

            IEnumerable<ParseError> ConvertValidityCheck(string[] validityCallback) =>
                isValid(validityCallback.Single());

            return WithPositionals(Convert, 1, 1, isRequired, ConvertValidityCheck);
        }

        public ParserBuilder WithPositionals(Action<object, string[]> consumeCallback, int min = 1,
            int max = int.MaxValue, bool isRequired = false, Func<string[], IEnumerable<ParseError>> isValid = null)
        {
            var positional = new Positional(consumeCallback)
            {
                Min = min,
                Max = max,
                ValidityCallback = isValid
            };
            Context.ParameterRepository.AddParameter(Name, positional);
            if (isRequired)
                Context.ValidatorRepository.AddValidator(Name, new RequiredParameterValidator(positional));
            return this;
        }

        public ParserBuilder WithSingleValueSwitch(char? letter, string word, Action<object, string> consumeCallback,
            bool isRequired = false, Func<string, IEnumerable<ParseError>> isValid = null)
        {
            var singleValueSwitch = new SingleValueSwitch(letter, word, consumeCallback)
            {
                ValidityCallback = isValid
            };
            Context.ParameterRepository.AddParameter(Name, singleValueSwitch);
            if (isRequired)
                Context.ValidatorRepository.AddValidator(Name, new RequiredParameterValidator(singleValueSwitch));
            return this;
        }

        public ParserBuilder WithValidation(IValidator validator)
        {
            Context.ValidatorRepository.AddValidator(Name, validator);
            return this;
        }

        public ParserBuilder WithValueSwitch(char? letter, string word, Action<object, string[]> consumeCallback,
            bool isRequired = false, Func<string[], IEnumerable<ParseError>> isValid = null)
        {
            var valuesSwitch = new ValuesSwitch(letter, word, consumeCallback)
            {
                ValidityCallback = isValid
            };
            Context.ParameterRepository.AddParameter(Name, valuesSwitch);
            if (isRequired)
                Context.ValidatorRepository.AddValidator(Name, new RequiredParameterValidator(valuesSwitch));
            return this;
        }

        public IGitContext Context { get; set; }

        public string Name { get; }

        public GitBuilder Parent { get; set; }
    }
}