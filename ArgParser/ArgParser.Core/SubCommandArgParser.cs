using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public class SubCommandArgParser<T, TParent> : ArgParser<T> where T : TParent
    {
        /// <inheritdoc />
        public SubCommandArgParser(Func<T> factoryFunction, ArgParser<TParent> parentParser = null) : base(factoryFunction)
        {
            ParentParser = parentParser;
        }

        public new SubCommandArgParser<T, TParent> WithPositional(Positional<T> positional)
        {
            base.WithPositional(positional);
            return this;
        }

        public new SubCommandArgParser<T, TParent> WithTokenSwitch(Switch<T> @switch)
        {
            base.WithTokenSwitch(@switch);
            return this;
        }

        public new SubCommandArgParser<T, TParent> WithName(string name)
        {
            Name = name;
            return this;
        }

        public override ParseResult Parse(string[] args)
        {
            Reset();
            var instance = FactoryFunction();
            var info = new IterationInfo(args, 0);
            while (!info.IsEnd)
            {
                if (SwitchStrategy.IsSwitch(Switches, info))
                {
                    info = SwitchStrategy.ConsumeSwitch(Switches, instance, info);
                    continue;
                }

                if (ParentParser?.SwitchStrategy.IsSwitch(ParentParser.Switches, info) ?? false)
                {
                    info = ParentParser.SwitchStrategy.ConsumeSwitch(ParentParser.Switches, instance, info);
                    continue;
                }

                if (SwitchStrategy.IsGroup(Switches, info))
                {
                    info = SwitchStrategy.ConsumeGroup(Switches, instance, info);
                    continue;
                }

                if (ParentParser?.SwitchStrategy.IsGroup(ParentParser.Switches, info) ?? false)
                {
                    info = ParentParser.SwitchStrategy.ConsumeGroup(ParentParser.Switches, instance, info);
                    continue;
                }

                if (PositionalStrategy.IsPositional(Positionals, info))
                {
                    info = PositionalStrategy.Consume(Positionals, instance, info);
                    continue;
                }

                if (ParentParser?.PositionalStrategy.IsPositional(ParentParser.Positionals, info) ?? false)
                    info = ParentParser.PositionalStrategy.Consume(ParentParser.Positionals, instance, info);
            }

            var validations = OrderOfAddition.Where(x => x.Validate != null).Select(x => x.Validate);
            foreach (var validation in validations) validation(args, instance, info.Errors);

            var parentValidations = ParentParser?.OrderOfAddition.Where(x => x.Validate != null).Select(x => x.Validate) ?? new Action<string[], TParent, IList<ParsingError>>[0];
            foreach (var validation in parentValidations) validation(args, instance, info.Errors);

            return info.Errors.Any() ? new ParseResult(instance, info.Errors) : new ParseResult(instance);
        }

        protected internal ArgParser<TParent> ParentParser { get; set; }
    }
}