using System;

namespace ArgParser.Flavors.Git
{
    public class ParserBuilder : ISubBuilder
    {
        /// <inheritdoc />
        public ParserBuilder(string parserName, GitBuilder parent, IGitContext context)
        {
            Name = parserName ?? throw new ArgumentNullException(nameof(parserName));
            Break = parent ?? throw new ArgumentNullException(nameof(parent));
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IGitContext Context { get; set; }

        public string Name { get; }

        public ParserBuilder WithBooleanCommand(char letter, string word, Action<object> consumeCallback)
        {
            Context.GitParameterRepository.AddParameter(Name, new BooleanSwitch(letter, word, consumeCallback));
            return this;
        }

        public ParserBuilder WithSingleValueSwitch(char letter, string word, Action<object, string> consumeCallback)
        {
            Context.GitParameterRepository.AddParameter(Name, new SingleValueSwitch(letter, word, consumeCallback));
            return this;
        }

        public ParserBuilder WithValueSwitch(char letter, string word, Action<object, string[]> consumeCallback)
        {
            Context.GitParameterRepository.AddParameter(Name, new ValuesSwitch(letter, word, consumeCallback));
            return this;
        }

        public ParserBuilder WithPositional(Action<object, string[]> consumeCallback)
        {
            Context.GitParameterRepository.AddParameter(Name, new Positional(consumeCallback));
            return this;
        }

        public ParserBuilder WithFactoryFunctions(params Func<object>[] factoryFunctions)
        {
            if(factoryFunctions == null)
                throw new ArgumentNullException(nameof(factoryFunctions));

            foreach (var factoryFunction in factoryFunctions)
            {
                Context.GitFactoryFunctionRepository.AddFactoryFunction(Name, factoryFunction);
            }
            return this;
        }

        public GitBuilder Break { get; }
    }
}