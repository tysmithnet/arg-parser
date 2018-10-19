// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-17-2018
// ***********************************************************************
// <copyright file="SubCommandArgParser.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    /// <summary>
    ///     Represents an object that is capable of indicating that a sub command has been requested
    ///     and for parsing the arguments
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TParent">The type of the t parent.</typeparam>
    /// <seealso cref="ArgParser.Core.ArgParser{T}" />
    public class SubCommandArgParser<T, TParent> : ArgParser<T> where T : TParent
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SubCommandArgParser{T, TParent}" /> class.
        /// </summary>
        /// <param name="factoryFunction">The factory function.</param>
        /// <param name="parentParser">The parent parser.</param>
        /// <inheritdoc />
        public SubCommandArgParser(Func<T> factoryFunction, ArgParser<TParent> parentParser = null) : base(
            factoryFunction)
        {
            ParentParser = parentParser;
        }

        /// <summary>
        ///     Parses the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>ParseResult.</returns>
        /// <exception cref="System.InvalidOperationException">
        ///     No forward progress detected. This likely means that there is an
        ///     input that can't be accounted for. Check your switches and positional rules.
        /// </exception>
        public override ParseResult Parse(string[] args)
        {
            Reset();
            var instance = FactoryFunction();
            IIterationInfo info = new IterationInfo(args, 0);
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

                if (ParentParser?.SwitchStrategy.IsGroup(ParentParser.Switches, info) ?? false) // todo: this doesn't account for groups that span child <-> parent gap
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

        /// <summary>
        ///     Set the name of the parser. Typically this is the name of the sub command.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>SubCommandArgParser&lt;T, TParent&gt;.</returns>
        public new SubCommandArgParser<T, TParent> WithName(string name)
        {
            Name = name;
            return this;
        }

        /// <summary>
        ///     Adds a positional to the pipe line
        /// </summary>
        /// <param name="positional">The positional.</param>
        /// <returns>SubCommandArgParser&lt;T, TParent&gt;.</returns>
        public new SubCommandArgParser<T, TParent> WithPositional(Positional<T> positional)
        {
            base.WithPositional(positional);
            return this;
        }

        /// <summary>
        ///     Adds a switch to the pipeline
        /// </summary>
        /// <param name="switch">The switch.</param>
        /// <returns>SubCommandArgParser&lt;T, TParent&gt;.</returns>
        public new SubCommandArgParser<T, TParent> WithSwitch(Switch<T> @switch)
        {
            base.WithSwitch(@switch);
            return this;
        }

        /// <summary>
        ///     Adds a validation function to the pipeline.
        /// </summary>
        /// <param name="validation">The validation.</param>
        /// <returns>SubCommandArgParser&lt;T, TParent&gt;.</returns>
        public new SubCommandArgParser<T, TParent> WithValidation(Validation validation)
        {
            base.WithValidation(validation);
            return this;
        }

        /// <summary>
        ///     Gets or sets the parent parser. This is the parser for the base options.
        /// </summary>
        /// <value>The parent parser.</value>
        protected internal ArgParser<TParent> ParentParser { get; set; }
    }
}