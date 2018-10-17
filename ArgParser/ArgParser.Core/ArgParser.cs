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
}
