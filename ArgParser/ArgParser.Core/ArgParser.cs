// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-18-2018
// ***********************************************************************
// <copyright file="ArgParser.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ArgParser.Core.Help;

namespace ArgParser.Core
{
    /// <summary>
    ///     Represents an object that is capable of building a parsing pipeline that will parse
    ///     arguments into an options object
    /// </summary>
    /// <typeparam name="TOptions">The type of options being created</typeparam>
    [DebuggerDisplay("{Name}")]
    public class ArgParser<TOptions>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ArgParser{T}" /> class.
        /// </summary>
        /// <param name="factoryFunction">The factory function.</param>
        /// <exception cref="ArgumentNullException">factoryFunction</exception>
        /// <exception cref="System.ArgumentNullException">factoryFunction</exception>
        /// <inheritdoc />
        public ArgParser(Func<TOptions> factoryFunction)
        {
            FactoryFunction = factoryFunction ?? throw new ArgumentNullException(nameof(factoryFunction));
            SetupStrategies();
        }

        /// <summary>
        ///     Parses the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>ParseResult.</returns>
        /// <exception cref="InvalidOperationException">No forward progress detected.</exception>
        /// <exception cref="System.InvalidOperationException">No forward progress detected.</exception>
        public virtual ParseResult Parse(string[] args)
        {
            Reset();
            var instance = FactoryFunction();
            IIterationInfo info = new IterationInfo(args, 0);
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

        /// <summary>
        ///     Resets this instance.
        /// </summary>
        public virtual void Reset()
        {
            SubCommandStrategy.Reset();
            PositionalStrategy.Reset();
            SwitchStrategy.Reset();
        }

        /// <summary>
        ///     Set the name for this arg parser. Typically this is the name of the application.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>ArgParser&lt;T&gt;.</returns>
        public ArgParser<TOptions> WithName(string name)
        {
            Name = name;
            return this;
        }

        /// <summary>
        ///     Adds a positional element to the pipe line
        /// </summary>
        /// <param name="positional">The positional.</param>
        /// <returns>ArgParser&lt;T&gt;.</returns>
        public virtual ArgParser<TOptions> WithPositional(Positional<TOptions> positional)
        {
            OrderOfAddition.Add(positional);
            return this;
        }

        /// <summary>
        ///     Adds a sub command to the pipe line
        /// </summary>
        /// <typeparam name="TSub">The type of the sub command's options</typeparam>
        /// <param name="subCommand">The sub command.</param>
        /// <returns>ArgParser&lt;T&gt;.</returns>
        public virtual ArgParser<TOptions> WithSubCommand<TSub>(SubCommand<TSub, TOptions> subCommand)
            where TSub : TOptions
        {
            subCommand.ArgParser.ParentParser = this;
            SubCommands.Add(subCommand);
            return this;
        }

        /// <summary>
        ///     Adds a switch to the pipeline
        /// </summary>
        /// <param name="switch">The switch.</param>
        /// <returns>ArgParser&lt;T&gt;.</returns>
        public virtual ArgParser<TOptions> WithSwitch(Switch<TOptions> @switch)
        {
            OrderOfAddition.Add(@switch);
            return this;
        }

        /// <summary>
        ///     Adds a validation function to the pipeline
        /// </summary>
        /// <param name="validation">The validation.</param>
        /// <returns>ArgParser&lt;T&gt;.</returns>
        public virtual ArgParser<TOptions> WithValidation(Validation validation)
        {
            Validations.Add(validation);
            return this;
        }

        /// <summary>
        ///     Setup the strategies.
        /// </summary>
        private void SetupStrategies()
        {
            var p = new DefaultPositionalStrategy<TOptions>();
            var s = new DefaultSwitchStrategy<TOptions>();
            p.Switches = Switches;
            s.Switches = Switches;
            p.SwitchStrategy = s;
            s.PositionalStrategy = p;
            s.Positionals = Positionals;
            SwitchStrategy = s;
            PositionalStrategy = p;
        }

        /// <summary>
        ///     Gets or sets the help builder.
        /// </summary>
        /// <value>The help builder.</value>
        public IHelpBuilder<TOptions> HelpBuilder { get; set; } = null;

        /// <summary>
        ///     Gets or sets the identity information.
        /// </summary>
        /// <value>The identity information.</value>
        public IdentityInformation IdentityInformation { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the name of the parser. Typically this is the name of the application or sub command.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the factory function. This function will be used to create new instances
        ///     of the options for each parsing
        /// </summary>
        /// <value>The factory function.</value>
        protected internal Func<TOptions> FactoryFunction { get; set; }

        /// <summary>
        ///     Gets or sets the order of addition. This is the history of pipeline elements added.
        /// </summary>
        /// <value>The order of addition.</value>
        protected internal virtual IList<PipelineElement<TOptions>> OrderOfAddition { get; set; } =
            new List<PipelineElement<TOptions>>();

        /// <summary>
        ///     Gets the positionals.
        /// </summary>
        /// <value>The positionals.</value>
        protected internal virtual IList<Positional<TOptions>> Positionals =>
            OrderOfAddition.OfType<Positional<TOptions>>().ToList();

        /// <summary>
        ///     Gets or sets the positional strategy.
        /// </summary>
        /// <value>The positional strategy.</value>
        protected internal virtual IPositionalStrategy<TOptions> PositionalStrategy { get; set; } =
            new DefaultPositionalStrategy<TOptions>();

        /// <summary>
        ///     Gets or sets the sub commands.
        /// </summary>
        /// <value>The sub commands.</value>
        protected internal virtual IList<ISubCommand> SubCommands { get; set; } = new List<ISubCommand>();

        /// <summary>
        ///     Gets or sets the sub command strategy.
        /// </summary>
        /// <value>The sub command strategy.</value>
        protected internal virtual ISubCommandStrategy<TOptions> SubCommandStrategy { get; set; } =
            new DefaultSubCommandStrategy<TOptions>();

        /// <summary>
        ///     Gets the switches.
        /// </summary>
        /// <value>The switches.</value>
        protected internal virtual IList<Switch<TOptions>> Switches =>
            OrderOfAddition.OfType<Switch<TOptions>>().ToList();

        /// <summary>
        ///     Gets or sets the switch strategy.
        /// </summary>
        /// <value>The switch strategy.</value>
        protected internal virtual ISwitchStrategy<TOptions> SwitchStrategy { get; set; } =
            new DefaultSwitchStrategy<TOptions>();

        /// <summary>
        ///     Gets or sets the validations.
        /// </summary>
        /// <value>The validations.</value>
        protected internal IList<Validation> Validations { get; set; } = new List<Validation>();

        /// <summary>
        ///     Delegate Validation
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="instance">The instance.</param>
        public delegate void Validation(IIterationInfo info, TOptions instance);
    }
}