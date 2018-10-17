using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ArgParser.Core
{
    public class ArgParser<T>
    {
        /// <inheritdoc />
        public ArgParser(Func<T> factoryFunction)
        {
            FactoryFunction = factoryFunction ?? throw new ArgumentNullException(nameof(factoryFunction));
        }

        public ParseResult Parse(string[] args)
        {
            Reset();
            var instance = FactoryFunction();
            var info = new IterationInfo(args, 0);
            while (!info.IsEnd)
            {
                if (SubCommandStrategy.IsSubCommand(info))
                    return SubCommandStrategy.Parse(info);
                if (SwitchStrategy.IsSwitch(info))
                {
                    info = SwitchStrategy.ConsumeSwitch(instance, info);
                    continue;
                }

                if (SwitchStrategy.IsGroup(info))
                {
                    info = SwitchStrategy.ConsumeGroup(instance, info);
                    continue;
                }
                if (PositionalStrategy.IsPositional(info)) info = PositionalStrategy.Consume(instance, info);
            }

            var validations = OrderOfAddition.Where(x => x.Validate != null).Select(x => x.Validate);
            foreach (var validation in validations)
            {
                validation(args, instance, info.Errors);
            }

            return info.Errors.Any() ? new ParseResult(instance, info.Errors) : new ParseResult(instance);
        }

        private void Reset()
        {
            SubCommandStrategy.Reset();
            PositionalStrategy.Reset();
            SwitchStrategy.Reset();
        }

        public virtual ArgParser<T> WithPositional(Positional<T> positional)
        {
            OrderOfAddition.Add(positional);
            return this;
        }

        public virtual ArgParser<T> WithSubCommand<TSub>(SubCommand<TSub> subCommand) where TSub : T
        {
            SubCommands.Add(subCommand);
            return this;
        }

        public virtual ArgParser<T> WithTokenSwitch(TokenSwitch<T> tokenSwitch)
        {
            OrderOfAddition.Add(tokenSwitch);
            return this;
        }

        protected internal Func<T> FactoryFunction { get; set; }

        protected internal virtual IList<CommandLineElement<T>> OrderOfAddition { get; set; } =
            new List<CommandLineElement<T>>();

        protected internal IPositionalStrategy<T> PositionalStrategy { get; set; } = new DefaultPositionalStrategy<T>();
        protected internal virtual IList<ISubCommand> SubCommands { get; set; } = new List<ISubCommand>();
        protected internal ISubCommandStrategy<T> SubCommandStrategy { get; set; } = new DefaultSubCommandStrategy<T>();
        protected internal ISwitchStrategy<T> SwitchStrategy { get; set; } = new DefaultSwitchStrategy<T>();
    }
}