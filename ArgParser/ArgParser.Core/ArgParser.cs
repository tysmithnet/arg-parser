using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ArgParser.Core
{
    public class SubCommandArgParser<T, TParent> : ArgParser<T> where T : TParent
    {
        /// <inheritdoc />
        public SubCommandArgParser(Func<T> factoryFunction, ArgParser<TParent> parentParser) : base(factoryFunction)
        {
            ParentParser = parentParser ?? throw new ArgumentNullException(nameof(parentParser));
        }

        public override ParseResult Parse(string[] args)
        {
            Reset();
            var instance = FactoryFunction();
            var info = new IterationInfo(args, 0);
            while (!info.IsEnd)
            {
                if (SubCommandStrategy.IsSubCommand(SubCommands, info))
                    return SubCommandStrategy.Parse(SubCommands, info);
                if (SwitchStrategy.IsSwitch(Switches, info))
                {
                    info = SwitchStrategy.ConsumeSwitch(Switches, instance, info);
                    continue;
                }

                if (ParentParser.SwitchStrategy.IsSwitch(ParentParser.Switches, info))
                {
                    info = ParentParser.SwitchStrategy.ConsumeSwitch(ParentParser.Switches, instance, info);
                    continue;
                }

                if (SwitchStrategy.IsGroup(Switches, info))
                {
                    info = SwitchStrategy.ConsumeGroup(Switches, instance, info);
                    continue;
                }

                if (ParentParser.SwitchStrategy.IsGroup(ParentParser.Switches, info))
                {
                    info = ParentParser.SwitchStrategy.ConsumeGroup(ParentParser.Switches, instance, info);
                    continue;
                }

                if (PositionalStrategy.IsPositional(Positionals, info))
                    info = PositionalStrategy.Consume(Positionals, instance, info);

                if (ParentParser.PositionalStrategy.IsPositional(ParentParser.Positionals, info))
                    info = ParentParser.PositionalStrategy.Consume(ParentParser.Positionals, instance, info);
            }

            var validations = OrderOfAddition.Where(x => x.Validate != null).Select(x => x.Validate);
            foreach (var validation in validations) validation(args, instance, info.Errors);

            var parentValidations = ParentParser.OrderOfAddition.Where(x => x.Validate != null).Select(x => x.Validate);
            foreach (var validation in parentValidations) validation(args, instance, info.Errors);

            return info.Errors.Any() ? new ParseResult(instance, info.Errors) : new ParseResult(instance);
        }

        protected internal ArgParser<TParent> ParentParser { get; set; }
    }

    [DebuggerDisplay("{Name}")]
    public class ArgParser<T>
    {
        /// <inheritdoc />
        public ArgParser(Func<T> factoryFunction)
        {
            FactoryFunction = factoryFunction ?? throw new ArgumentNullException(nameof(factoryFunction));
        }

        public virtual ParseResult Parse(string[] args)
        {
            Reset();
            var instance = FactoryFunction();
            var info = new IterationInfo(args, 0);
            while (!info.IsEnd)
            {
                if (SubCommandStrategy.IsSubCommand(SubCommands, info))
                    return SubCommandStrategy.Parse(SubCommands, info);
                if (SwitchStrategy.IsSwitch(Switches, info))
                {
                    info = SwitchStrategy.ConsumeSwitch(Switches, instance, info);
                    continue;
                }

                if (SwitchStrategy.IsGroup(Switches, info))
                {
                    info = SwitchStrategy.ConsumeGroup(Switches, instance, info);
                    continue;
                }

                if (PositionalStrategy.IsPositional(Positionals, info))
                    info = PositionalStrategy.Consume(Positionals, instance, info);
            }

            var validations = OrderOfAddition.Where(x => x.Validate != null).Select(x => x.Validate);
            foreach (var validation in validations) validation(args, instance, info.Errors);

            return info.Errors.Any() ? new ParseResult(instance, info.Errors) : new ParseResult(instance);
        }

        public virtual void Reset()
        {
            SubCommandStrategy.Reset();
            PositionalStrategy.Reset();
            SwitchStrategy.Reset();
        }

        public ArgParser<T> WithName(string name)
        {
            Name = name;
            return this;
        }

        public virtual ArgParser<T> WithPositional(Positional<T> positional)
        {
            OrderOfAddition.Add(positional);
            return this;
        }

        public virtual ArgParser<T> WithSubCommand<TSub>(SubCommand<TSub> subCommand) where TSub : T
        {
            var newParser = new SubCommandArgParser<TSub, T>(subCommand.ArgParser.FactoryFunction, this)
            {
                Name = subCommand.ArgParser.Name
            };

            var newSubCommand = new SubCommand<TSub>
            {
                IsCommand = subCommand.IsCommand,
                ArgParser = newParser
            };
            SubCommands.Add(newSubCommand);
            return this;
        }

        public virtual ArgParser<T> WithTokenSwitch(Switch<T> @switch)
        {
            OrderOfAddition.Add(@switch);
            return this;
        }

        public string Name { get; protected internal set; }

        protected internal Func<T> FactoryFunction { get; set; }

        protected internal virtual IList<CommandLineElement<T>> OrderOfAddition { get; set; } =
            new List<CommandLineElement<T>>();

        protected internal virtual IList<Positional<T>> Positionals => OrderOfAddition.OfType<Positional<T>>().ToList();

        protected internal virtual IPositionalStrategy<T> PositionalStrategy { get; set; } =
            new DefaultPositionalStrategy<T>();

        protected internal virtual IList<ISubCommand> SubCommands { get; set; } = new List<ISubCommand>();

        protected internal virtual ISubCommandStrategy<T> SubCommandStrategy { get; set; } =
            new DefaultSubCommandStrategy<T>();

        protected internal virtual IList<Switch<T>> Switches => OrderOfAddition.OfType<Switch<T>>().ToList();
        protected internal virtual ISwitchStrategy<T> SwitchStrategy { get; set; } = new DefaultSwitchStrategy<T>();
    }
}