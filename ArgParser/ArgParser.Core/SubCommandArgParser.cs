using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public class SubCommandArgParser<T, TParent> : ArgParser<T> where T : TParent
    {
        /// <inheritdoc />
        public SubCommandArgParser(Func<T> factoryFunction, ArgParser<TParent> parentParser = null) : base(
            factoryFunction)
        {
            ParentParser = parentParser;
        }

        public override ParseResult Parse(string[] args)
        {
            Reset();
            var instance = FactoryFunction();
            var info = new IterationInfo(args, 0);
            var history = new List<int>();
            while (!info.IsEnd)
            {
                if (history.Count > 1 && history.First() >= info.Index)
                    throw new InvalidOperationException(
                        "No forward progress detected. This likely means that there is an input that can't be accounted for. Check your switches and positional rules.");
                history.Insert(0, info.Index);
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
                {
                    info = PositionalStrategy.Consume(Positionals, instance, info);
                    continue;
                }

                if (ParentParser?.SwitchStrategy.IsSwitch(ParentParser.Switches, info) ?? false)
                {
                    info = ParentParser.SwitchStrategy.ConsumeSwitch(ParentParser.Switches, instance, info);
                    continue;
                }

                if (ParentParser?.SwitchStrategy.IsGroup(ParentParser.Switches, info) ?? false)
                {
                    info = ParentParser.SwitchStrategy.ConsumeGroup(ParentParser.Switches, instance, info);
                    continue;
                }

                if (ParentParser?.PositionalStrategy.IsPositional(ParentParser.Positionals, info) ?? false)
                    info = ParentParser.PositionalStrategy.Consume(ParentParser.Positionals, instance, info);
            }

            foreach (var validation in Validations) validation(info, instance);

            foreach (var validation in ParentParser?.Validations ?? new List<ArgParser<TParent>.Validation>())
                validation(info, instance);

            return new ParseResult(instance, info);
        }

        public new SubCommandArgParser<T, TParent> WithName(string name)
        {
            Name = name;
            return this;
        }

        public new SubCommandArgParser<T, TParent> WithPositional(Positional<T> positional)
        {
            base.WithPositional(positional);
            return this;
        }

        public new SubCommandArgParser<T, TParent> WithSwitch(Switch<T> @switch)
        {
            base.WithSwitch(@switch);
            return this;
        }

        public new SubCommandArgParser<T, TParent> WithValidation(Validation validation)
        {
            base.WithValidation(validation);
            return this;
        }

        protected internal ArgParser<TParent> ParentParser { get; set; }
    }
}