using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ArgParser.Core
{
    public class ArgParser<T>
    {
        protected internal virtual IList<CommandLineElement<T>> OrderOfAddition { get; set; } = new List<CommandLineElement<T>>();

        protected internal virtual IList<object> SubCommands { get; set; } = new List<object>();

        /// <inheritdoc />
        public ArgParser(Func<T> factoryFunction)
        {
            FactoryFunction = factoryFunction ?? throw new ArgumentNullException(nameof(factoryFunction));
        }

        public virtual Func<T> FactoryFunction { get; protected internal set; }
        
        public virtual ArgParser<T> WithPositional(Positional<T> positional)
        {
            OrderOfAddition.Add(positional);
            return this;
        }

        public virtual ArgParser<T> WithTokenSwitch(TokenSwitch<T> tokenSwitch)
        {
            OrderOfAddition.Add(tokenSwitch);
            return this;
        }

        public virtual ArgParser<T> WithSubCommand<TSub>(SubCommand<TSub> subCommand) where TSub : T
        {
            SubCommands.Add(subCommand);
            return this;
        }

        public ParseResult<T> Parse(string[] args)
        {
            return new ParseResult<T>();
        }
    }
}
