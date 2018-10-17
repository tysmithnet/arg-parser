using System;
using System.Collections.Generic;
using System.Text;

namespace ArgParser.Core
{
    public class ArgParser<T>
    {
        protected internal IList<CommandLineElement<T>> OrderOfAddition { get; set; } = new List<CommandLineElement<T>>();

        /// <inheritdoc />
        public ArgParser(Func<T> factoryFunction)
        {
            FactoryFunction = factoryFunction;
        }

        public Func<T> FactoryFunction { get; protected internal set; }

        public ArgParser<T> With()
        {
            return this;
        }

        public ArgParser<T> WithPositional(Positional<T> positional)
        {
            return this;
        }

        public ArgParser<T> WithTokenSwitch(TokenSwitch<T> tokenSwitch)
        {
            return this;
        }

        public ArgParser<T> WithSubCommand<TSub>(string command, ArgParser<TSub> parser) where TSub : T
        {
            return this;
        }

        public ParseResult<T> Parse(string[] args)
        {
            return new ParseResult<T>();
        }
    }

    public class ParseResult<T>
    {
        public ParseResult<T> When<TSub>(Action<TSub> handler) where TSub : T
        {
            return this;
        }
    }

    public abstract class CommandLineElement<T>
    {
        public Func<IterationInfo, string, int, bool> TakeWhile { get; set; }
        public Action<IterationInfo, T, string[]> Transformer { get; set; }
        public Action<IterationInfo, T, string[], IList<ParsingError>> Validate { get; set; }
    }

    public class Positional<T> : CommandLineElement<T>
    {
    }

    public class TokenSwitch<T>
    {
        public char? GroupLetter { get; set; }
        public Func<IterationInfo, bool> IsToken { get; set; }
        public Func<IterationInfo, string, int, bool> TakeWhile { get; set; }
        public Action<IterationInfo, T, string[]> Transformer { get; set; }
        public Action<IterationInfo, T, string[], IList<ParsingError>> Validate { get; set; }

    }

    public class ParsingError
    {
        public string Message { get; set; }

        /// <inheritdoc />
        public ParsingError(string message)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }
    }

    public class FormatError : ParsingError
    {
        /// <inheritdoc />
        public FormatError(string message) : base(message)
        {
        }
    }

    public class CardinalityError : ParsingError
    {
        /// <inheritdoc />
        public CardinalityError(string message) : base(message)
        {
        }
    }
}
