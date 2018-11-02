using System;
using System.Collections.Generic;
using System.Text;

namespace ArgParser.Flavors.Git
{
    public interface IGitContext
    {
        IGitParserRepository GitParserRepository { get; }
        IGitParameterRepository GitParameterRepository { get; }
        IGitValidatorRepository GitValidatorRepository { get; }
        IGitFactoryFunctionRepository GitFactoryFunctionRepository { get; }
    }

    public class GitContext : IGitContext
    {
        public IGitParserRepository GitParserRepository { get; set; } = new GitParserRepository();
        public IGitParameterRepository GitParameterRepository { get; set; } = new GitParameterRepository();
        public IGitValidatorRepository GitValidatorRepository { get; set; } = new GitValidatorRepository();
        public IGitFactoryFunctionRepository GitFactoryFunctionRepository { get; set; } = new GitFactoryFunctionRepository();
    }

    public class GitBuilder
    {
        public IGitContext GitContext { get; set; } = new GitContext();
        public ParserBuilder AddParser(string name)
        {
            return new ParserBuilder(name, this, GitContext);
        }
    }

    public interface ISubBuilder
    {
        GitBuilder Break { get; }
    }

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

        public GitBuilder Break { get; }
    }
}
