// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-12-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="ParserBuilder.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles
{
    

    /// <summary>
    ///     Builder pattern for Parser
    /// </summary>
    public class ParserBuilder
    {
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="ParserBuilder" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="parser">The parser.</param>
        public ParserBuilder(ContextBuilder parent, Parser parser)
        {
            Finish = parent.ThrowIfArgumentNull(nameof(parent));
            Parser = parser.ThrowIfArgumentNull(nameof(parser));
        }

        /// <summary>
        ///     Sets the alias
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>ParserBuilder.</returns>
        public virtual ParserBuilder WithAlias(string alias)
        {
            Finish.AliasRepository.SetAlias(Parser.Id, alias);
            return this;
        }

        /// <summary>
        ///     Adds a boolean switch
        /// </summary>
        /// <param name="letter">The letter.</param>
        /// <param name="word">The word.</param>
        /// <param name="consumeCallback">The consume callback.</param>
        /// <param name="helpSetupCallback">The help setup callback.</param>
        /// <param name="required">if set to <c>true</c> [required].</param>
        /// <returns>ParserBuilder.</returns>
        public virtual ParserBuilder WithBooleanSwitch(char? letter, string word, Action<object> consumeCallback,
            Action<ParameterHelpBuilder> helpSetupCallback = null, bool required = false)
        {
            var sw = new BooleanSwitch(Parser, letter, word, consumeCallback)
            {
                IsRequired = required
            };
            AddParameterInternal(sw, helpSetupCallback);
            return this;
        }

        /// <summary>
        ///     Sets the factory function
        /// </summary>
        /// <param name="func">The function.</param>
        /// <returns>ParserBuilder.</returns>
        public virtual ParserBuilder WithFactoryFunction(Func<object> func)
        {
            Parser.FactoryFunction = func.ThrowIfArgumentNull(nameof(func));
            return this;
        }

        /// <summary>
        ///     Adds a positional that has only 1 value
        /// </summary>
        /// <param name="consumeCallback">The consume callback.</param>
        /// <param name="helpSetupCallback">The help setup callback.</param>
        /// <param name="required">if set to <c>true</c> [required].</param>
        /// <returns>ParserBuilder.</returns>
        public ParserBuilder WithPositional(Action<object, string> consumeCallback,
            Action<ParameterHelpBuilder> helpSetupCallback = null, bool required = false)
        {
            var sw = new Positional(Parser, (o, strings) => consumeCallback(o, strings.Single()), 1, 1)
            {
                IsRequired = required
            };
            AddParameterInternal(sw, helpSetupCallback);
            return this;
        }

        /// <summary>
        ///     Adds a positional that can have multiple values
        /// </summary>
        /// <param name="consumeCallback">The consume callback.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <param name="helpSetupCallback">The help setup callback.</param>
        /// <param name="required">if set to <c>true</c> [required].</param>
        /// <returns>ParserBuilder.</returns>
        public ParserBuilder WithPositionals(Action<object, string[]> consumeCallback, int min = 1,
            int max = int.MaxValue, Action<ParameterHelpBuilder> helpSetupCallback = null, bool required = false)
        {
            var sw = new Positional(Parser, consumeCallback, min, max)
            {
                IsRequired = required
            };
            AddParameterInternal(sw, helpSetupCallback);
            return this;
        }

        /// <summary>
        ///     Adds a SingleValueSwitch
        /// </summary>
        /// <param name="letter">The letter.</param>
        /// <param name="word">The word.</param>
        /// <param name="consumeCallback">The consume callback.</param>
        /// <param name="helpSetupCallback">The help setup callback.</param>
        /// <param name="required">if set to <c>true</c> [required].</param>
        /// <returns>ParserBuilder.</returns>
        public ParserBuilder WithSingleValueSwitch(char? letter, string word, Action<object, string> consumeCallback,
            Action<ParameterHelpBuilder> helpSetupCallback = null, bool required = false)
        {
            var sw = new SingleValueSwitch(Parser, letter, word, consumeCallback)
            {
                MinRequired = 1,
                MaxAllowed = 1,
                IsRequired = required
            };
            AddParameterInternal(sw, helpSetupCallback);
            return this;
        }

        /// <summary>
        ///     Adds a ValuesSwitch
        /// </summary>
        /// <param name="letter">The letter.</param>
        /// <param name="word">The word.</param>
        /// <param name="consumeCallback">The consume callback.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <param name="helpSetupCallback">The help setup callback.</param>
        /// <param name="required">if set to <c>true</c> [required].</param>
        /// <returns>ParserBuilder.</returns>
        public ParserBuilder WithValuesSwitch(char? letter, string word, Action<object, string[]> consumeCallback,
            int min = 1, int max = int.MaxValue, Action<ParameterHelpBuilder> helpSetupCallback = null,
            bool required = false)
        {
            var sw = new ValuesSwitch(Parser, letter, word, consumeCallback)
            {
                MinRequired = min,
                MaxAllowed = max,
                IsRequired = required
            };
            AddParameterInternal(sw, helpSetupCallback);
            return this;
        }

        /// <summary>
        ///     Adds the parameter
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="helpSetupCallback">The help setup callback.</param>
        protected void AddParameterInternal(Parameter parameter, Action<ParameterHelpBuilder> helpSetupCallback = null)
        {
            Parser.AddParameter(parameter);
            if (helpSetupCallback != null)
            {
                var builder = new ParameterHelpBuilder(parameter);
                helpSetupCallback(builder);
                parameter.Help = builder.Build();
            }
        }

        /// <summary>
        ///     Gets or sets the finish.
        /// </summary>
        /// <value>The finish.</value>
        public ContextBuilder Finish { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the parser.
        /// </summary>
        /// <value>The parser.</value>
        public Parser Parser { get; protected internal set; }
    }

    /// <summary>
    ///     Class ParserBuilder.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ArgParser.Styles.ParserBuilder" />
    public class ParserBuilder<T> : ParserBuilder
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ParserBuilder{T}" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="parser">The parser.</param>
        public ParserBuilder(ContextBuilder parent, Parser<T> parser) : base(parent, parser)
        {
            Finish = parent.ThrowIfArgumentNull(nameof(parent));
            Parser = parser.ThrowIfArgumentNull(nameof(parser));
        }

        /// <summary>
        ///     Sets the alias
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>ParserBuilder&lt;T&gt;.</returns>
        public new ParserBuilder<T> WithAlias(string alias)
        {
            base.WithAlias(alias);
            return this;
        }

        /// <summary>
        ///     Adds a boolean switch
        /// </summary>
        /// <param name="letter">The letter.</param>
        /// <param name="word">The word.</param>
        /// <param name="consumeCallback">The consume callback.</param>
        /// <param name="helpSetupCallback">The help setup callback.</param>
        /// <param name="required">if set to <c>true</c> [required].</param>
        /// <returns>ParserBuilder&lt;T&gt;.</returns>
        public ParserBuilder<T> WithBooleanSwitch(char? letter, string word, Action<T> consumeCallback,
            Action<ParameterHelpBuilder> helpSetupCallback = null, bool required = false)
        {
            var sw = new BooleanSwitch<T>(Parser, letter, word, consumeCallback)
            {
                IsRequired = required
            };
            AddParameterInternal(sw, helpSetupCallback);
            return this;
        }

        /// <summary>
        ///     Sets the factory function
        /// </summary>
        /// <param name="func">The function.</param>
        /// <returns>ParserBuilder&lt;T&gt;.</returns>
        public ParserBuilder<T> WithFactoryFunction(Func<T> func)
        {
            Parser.FactoryFunction = func.ThrowIfArgumentNull(nameof(func));
            return this;
        }

        /// <summary>
        ///     Adds a positional with 1 value
        /// </summary>
        /// <param name="consumeCallback">The consume callback.</param>
        /// <param name="helpSetupCallback">The help setup callback.</param>
        /// <param name="required">if set to <c>true</c> [required].</param>
        /// <returns>ParserBuilder&lt;T&gt;.</returns>
        public ParserBuilder<T> WithPositional(Action<T, string> consumeCallback,
            Action<ParameterHelpBuilder> helpSetupCallback = null, bool required = false)
        {
            var sw = new Positional<T>(Parser, (o, strings) => consumeCallback(o, strings.Single()), 1, 1)
            {
                IsRequired = required
            };
            AddParameterInternal(sw, helpSetupCallback);
            return this;
        }

        /// <summary>
        ///     Adds a positional with multiple values
        /// </summary>
        /// <param name="consumeCallback">The consume callback.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <param name="helpSetupCallback">The help setup callback.</param>
        /// <param name="required">if set to <c>true</c> [required].</param>
        /// <returns>ParserBuilder&lt;T&gt;.</returns>
        public ParserBuilder<T> WithPositionals(Action<T, string[]> consumeCallback, int min = 1,
            int max = int.MaxValue, Action<ParameterHelpBuilder> helpSetupCallback = null, bool required = false)
        {
            var sw = new Positional<T>(Parser, consumeCallback, min, max)
            {
                IsRequired = required
            };
            AddParameterInternal(sw, helpSetupCallback);
            return this;
        }

        /// <summary>
        ///     Adds a SingleValueSwitch
        /// </summary>
        /// <param name="letter">The letter.</param>
        /// <param name="word">The word.</param>
        /// <param name="consumeCallback">The consume callback.</param>
        /// <param name="helpSetupCallback">The help setup callback.</param>
        /// <param name="required">if set to <c>true</c> [required].</param>
        /// <returns>ParserBuilder&lt;T&gt;.</returns>
        public ParserBuilder<T> WithSingleValueSwitch(char? letter, string word, Action<T, string> consumeCallback,
            Action<ParameterHelpBuilder> helpSetupCallback = null, bool required = false)
        {
            var sw = new SingleValueSwitch<T>(Parser, letter, word, consumeCallback)
            {
                IsRequired = required
            };
            AddParameterInternal(sw, helpSetupCallback);
            return this;
        }

        /// <summary>
        ///     Adds a ValuesSwitch
        /// </summary>
        /// <param name="letter">The letter.</param>
        /// <param name="word">The word.</param>
        /// <param name="consumeCallback">The consume callback.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <param name="helpSetupCallback">The help setup callback.</param>
        /// <param name="required">if set to <c>true</c> [required].</param>
        /// <returns>ParserBuilder&lt;T&gt;.</returns>
        public ParserBuilder<T> WithValuesSwitch(char? letter, string word, Action<T, string[]> consumeCallback,
            int min = 1, int max = int.MaxValue, Action<ParameterHelpBuilder> helpSetupCallback = null,
            bool required = false)
        {
            var sw = new ValuesSwitch<T>(Parser, letter, word, consumeCallback)
            {
                IsRequired = required
            };
            AddParameterInternal(sw, helpSetupCallback);
            return this;
        }

        /// <summary>
        ///     Gets or sets the parser.
        /// </summary>
        /// <value>The parser.</value>
        public new Parser<T> Parser
        {
            get => base.Parser as Parser<T>;
            set => base.Parser = value;
        }
    }
}