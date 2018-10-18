using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ArgParser.Core
{
    [DebuggerDisplay("{Name}")]
    public class ArgParser<T>
    {
        /// <inheritdoc />
        public ArgParser(Func<T> factoryFunction)
        {
            FactoryFunction = factoryFunction ?? throw new ArgumentNullException(nameof(factoryFunction));
            SetupStrategies();
        }

        private void SetupStrategies()
        {
            var p = new DefaultPositionalStrategy<T>();
            var s = new DefaultSwitchStrategy<T>();
            p.Switches = Switches;
            s.Switches = Switches;
            p.SwitchStrategy = s;
            s.PositionalStrategy = p;
            s.Positionals = Positionals;
            SwitchStrategy = s;
            PositionalStrategy = p;
        }

        public virtual ParseResult Parse(string[] args)
        {
            Reset();
            var instance = FactoryFunction();
            var info = new IterationInfo(args, 0);
            var history = new List<int>();
            while (!info.IsEnd)
            {
                if (history.Count > 1 && history.First() >= info.Index)
                    throw new InvalidOperationException("No forward progress detected.");
                history.Insert(0, info.Index);
                if (info.Index == 0 && SubCommandStrategy.IsSubCommand(SubCommands, info))
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

            foreach (var validation in Validations) validation(info, instance);

            return new ParseResult(instance, info);
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

        public virtual ArgParser<T> WithSubCommand<TSub>(SubCommand<TSub, T> subCommand) where TSub : T
        {
            subCommand.ArgParser.ParentParser = this;
            SubCommands.Add(subCommand);
            return this;
        }

        public virtual ArgParser<T> WithSwitch(Switch<T> @switch)
        {
            OrderOfAddition.Add(@switch);
            return this;
        }

        public virtual ArgParser<T> WithValidation(Validation validation)
        {
            Validations.Add(validation);
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

        protected internal IList<Validation> Validations { get; set; } = new List<Validation>();

        public delegate void Validation(IterationInfo info, T instance);
    }
}