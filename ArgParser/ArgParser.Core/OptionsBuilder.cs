// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-15-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-15-2018
// ***********************************************************************
// <copyright file="OptionsBuilder.cs" company="ArgParser.Core">
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
    /// Class OptionsBuilder.
    /// </summary>
    /// <typeparam name="TOptions">The type of the t options.</typeparam>
    public class OptionsBuilder<TOptions> where TOptions : IOptions
    {
        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>The order.</value>
        internal IList<object> Order { get; set; } = new List<object>();
        /// <summary>
        /// The switches
        /// </summary>
        internal Dictionary<string, Switch<TOptions>> Switches = new Dictionary<string, Switch<TOptions>>();
        /// <summary>
        /// The positionals
        /// </summary>
        internal IList<PositionalValues<TOptions>> Positionals = new List<PositionalValues<TOptions>>();
        /// <summary>
        /// Withes the boolean.
        /// </summary>
        /// <param name="letter">The letter.</param>
        /// <param name="transformer">The transformer.</param>
        /// <returns>OptionsBuilder&lt;TOptions&gt;.</returns>
        public OptionsBuilder<TOptions> WithBoolean(char letter, Action<TOptions> transformer)
        {
            var newGuy = new BooleanSwitch<TOptions>()
            {
                Letter = letter,
                Transformer = transformer
            };
            Switches.Add($"-{letter}", newGuy);
            Order.Add(newGuy);
            return this;
        }

        /// <summary>
        /// Parses the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="args">The arguments.</param>
        /// <exception cref="ArgParser.Core.MissingValueException">
        /// </exception>
        public void Parse(TOptions instance, string[] args)
        {
            if (args == null || args.Length == 0)
                return;
            var positionalCounts = new Dictionary<PositionalValues<TOptions>, int>();
            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];

                if (Switches.ContainsKey(arg))
                {
                    var s = Switches[arg];
                    if (s is BooleanSwitch<TOptions> boolSwitch)
                    {
                        boolSwitch.Transformer(instance);
                        continue;
                    }

                    if (s is SingleSwitch<TOptions> singleSwitch)
                    {
                        if(i + 1 >= args.Length)
                            throw new MissingValueException($"Switch {arg} requires a value, but none was found.");
                        var nextArg = args[i + 1];
                        if(Switches.ContainsKey(nextArg))
                            throw new MissingValueException($"Switch {arg} requires a value, but found another switch: {nextArg}");
                        singleSwitch.Transformer(instance, nextArg);
                    }

                    if (s is MultipleSwitch<TOptions> multipleSwitch)
                    {
                        if (multipleSwitch.Count < 0)
                        {
                            var rest = args.Skip(i + 1).TakeWhile(a => !Switches.ContainsKey(a)).ToArray();
                            multipleSwitch.Transformer(instance, rest);
                            i += rest.Length;
                            continue;
                        }
                        else
                        {
                            int j;
                            List<string> multipleStrings = new List<string>();
                            for (j = 0; i + j + 1 < args.Length && j < multipleSwitch.Count; j++)
                            {
                                var cur = args[i + j + 1];
                                if (Switches.ContainsKey(cur))
                                    throw new MissingValueException($"Switch {arg} requires a value, but found another switch: {cur}");
                                multipleStrings.Add(cur);
                            }
                            if(multipleStrings.Count != multipleSwitch.Count)
                                throw new MissingValueException($"Switch {arg} requires {multipleSwitch.Count} values, but only found {multipleStrings.Count}");
                            multipleSwitch.Transformer(instance, multipleStrings.ToArray());
                            i += multipleStrings.Count;
                        }
                    }
                }
                else
                {
                    var positional = Order.OfType<PositionalValues<TOptions>>().FirstOrDefault(x => !positionalCounts.ContainsKey(x));
                    if(positional == null)
                        continue;
                    string[] newValues;
                    if (positional.Count < 0)
                    {
                        newValues = args.Skip(i).TakeWhile(x => !Switches.ContainsKey(x)).ToArray();
                    }
                    else
                    {
                        newValues = args.Skip(i).TakeWhile(x => !Switches.ContainsKey(x)).Take(positional.Count).ToArray();
                        if (newValues.Length != positional.Count)
                            throw new MissingValueException($"Positional argument requires {positional.Count} values, but found {newValues.Length}");
                    }
                    positional.Transformer(instance, newValues);
                    positionalCounts.Add(positional, newValues.Length);
                    i += newValues.Length - 1;
                }
            }
        }

        /// <summary>
        /// Withes the single switch.
        /// </summary>
        /// <param name="letter">The letter.</param>
        /// <param name="transformer">The transformer.</param>
        /// <returns>OptionsBuilder&lt;TOptions&gt;.</returns>
        public OptionsBuilder<TOptions> WithSingleSwitch(char letter, Action<TOptions, string> transformer)
        {
            var newGuy = new SingleSwitch<TOptions>()
            {
                Letter = letter,
                Transformer = transformer
            };
            Switches.Add($"-{letter}", newGuy);
            Order.Add(newGuy);
            return this;
        }

        /// <summary>
        /// Withes the multiple switch.
        /// </summary>
        /// <param name="letter">The letter.</param>
        /// <param name="transformer">The transformer.</param>
        /// <param name="count">The count.</param>
        /// <returns>OptionsBuilder&lt;TOptions&gt;.</returns>
        public OptionsBuilder<TOptions> WithMultipleSwitch(char letter, Action<TOptions, string[]> transformer, int count = -1)
        {
            var newGuy = new MultipleSwitch<TOptions>()
            {
                Letter = letter,
                Transformer = transformer,
                Count = count
            };
            Switches.Add($"-{letter}", newGuy);
            Order.Add(newGuy);
            return this;
        }

        /// <summary>
        /// Withes the positional.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <param name="transformer">The transformer.</param>
        /// <returns>OptionsBuilder&lt;TOptions&gt;.</returns>
        public OptionsBuilder<TOptions> WithPositional(int count, Action<TOptions, string[]> transformer)
        {
            var newGuy = new PositionalValues<TOptions>()
            {
                Count = count,
                Transformer = transformer
            };
            Positionals.Add(newGuy);
            Order.Add(newGuy);
            return this;
        }
    }
}
