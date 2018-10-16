// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-15-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-16-2018
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
    ///     Class OptionsBuilder.
    /// </summary>
    /// <typeparam name="TOptions">The type of the t options.</typeparam>
    public class ArgParser<TOptions> where TOptions : IOptions
    {
        /// <summary>
        ///     The positionals
        /// </summary>
        internal IList<PositionalValues<TOptions>> Positionals = new List<PositionalValues<TOptions>>();

        /// <summary>
        ///     The switches
        /// </summary>
        internal Dictionary<string, Switch<TOptions>> Switches = new Dictionary<string, Switch<TOptions>>();

        /// <summary>
        ///     Parses the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="args">The arguments.</param>
        /// <exception cref="ArgumentNullException">
        ///     instance
        ///     or
        ///     args
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        ///     instance
        ///     or
        ///     args
        /// </exception>
        /// <exception cref="ArgParser.Core.MissingValueException">
        ///     instance
        ///     or
        ///     args
        /// </exception>
        public void Parse(TOptions instance, string[] args)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));
            if (args == null)
                throw new ArgumentNullException(nameof(args));
            var positionalCounts = new Dictionary<PositionalValues<TOptions>, int>();
            for (var i = 0; i < args.Length; i++)
            {
                var arg = args[i];

                if (Switches.ContainsKey(arg))
                {
                    var s = Switches[arg];
                    switch (s)
                    {
                        case BooleanSwitch<TOptions> boolSwitch:
                            boolSwitch.Transformer(instance);
                            continue;
                        case SingleSwitch<TOptions> singleSwitch:
                            ExtractSingleSwitch(instance, args, i, arg, singleSwitch);
                            break;
                        case MultipleSwitch<TOptions> multipleSwitch:
                            i = multipleSwitch.Max.HasValue
                                ? ExtractMultipleSwitch(instance, args, i, multipleSwitch, arg)
                                : ExtractGreedyMultipleSwitch(instance, args, i, multipleSwitch);
                            break;
                    }
                }
                else if (IsGroupOfBoolean(arg))
                {
                    foreach (var letter in arg.ToCharArray().Skip(1).Distinct())
                    {
                        var boolean = (BooleanSwitch<TOptions>) Switches[$"-{letter}"];
                        boolean.Transformer(instance);
                    }
                }
                else
                {
                    i = ExtractPositional(instance, args, positionalCounts, i);
                }
            }

            var errors = new List<string>();
            foreach (var validator in ValidationMethods) validator(instance, errors);

            if (errors.Any()) throw new ValidationFailureException(errors);
        }

        public ArgParser<TOptions> WithBoolean(BooleanSwitch<TOptions> newGuy)
        {
            if (newGuy.Letter.HasValue)
            {
                Switches.Add($"-{newGuy.Letter.Value}", newGuy);
            }

            if (newGuy.Word != null)
            {
                Switches.Add($"--{newGuy.Word}", newGuy);
            }
            Order.Add(newGuy);
            return this;
        }

        public ArgParser<TOptions> WithMultipleSwitch(MultipleSwitch<TOptions> multipleSwitch)
        {
            if (multipleSwitch.Letter.HasValue)
            {
                Switches.Add($"-{multipleSwitch.Letter.Value}", multipleSwitch);
            }

            if (multipleSwitch.Word != null)
            {
                Switches.Add($"--{multipleSwitch.Word}", multipleSwitch);
            }
            Order.Add(multipleSwitch);
            return this;
        }

        public ArgParser<TOptions> WithPositional(PositionalValues<TOptions> newGuy)
        {
            Positionals.Add(newGuy);
            Order.Add(newGuy);
            return this;
        }

        public ArgParser<TOptions> WithSingleSwitch(SingleSwitch<TOptions> newGuy)
        {
            if (newGuy.Letter.HasValue) Switches.Add($"-{newGuy.Letter.Value}", newGuy);

            if (newGuy.Word != null) Switches.Add($"--{newGuy.Word}", newGuy);
            Order.Add(newGuy);
            return this;
        }

        public ArgParser<TOptions> WithValidation(Action<TOptions, IList<string>> validationMethod)
        {
            ValidationMethods.Add(validationMethod);
            return this;
        }

        /// <summary>
        ///     Extracts the multiple switch.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="i">The i.</param>
        /// <param name="multipleSwitch">The multiple switch.</param>
        /// <param name="arg">The argument.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="MissingValueException">
        /// </exception>
        internal int ExtractMultipleSwitch(TOptions instance, string[] args, int i,
            MultipleSwitch<TOptions> multipleSwitch, string arg)
        {
            var multipleStrings = new List<string>();
            for (var j = 0; i + j + 1 < args.Length; j++)
            {
                var cur = args[i + j + 1];
                if (Switches.ContainsKey(cur) || IsGroupOfBoolean(cur))
                    throw new MissingValueException($"Switch {arg} requires a value, but found another switch: {cur}");
                multipleStrings.Add(cur);
                if (multipleSwitch.Max.HasValue && multipleSwitch.Max.Value >= multipleStrings.Count)
                {
                    break;
                }
            }

            if(multipleSwitch.Min.HasValue)
                throw new MissingValueException(
                    $"Switch {arg} requires at least {multipleSwitch.Min.Value} values, but only found {multipleStrings.Count}");

            if (multipleSwitch.Max.HasValue)
                throw new MissingValueException(
                    $"Switch {arg} requires at most {multipleSwitch.Max.Value} values, but found {multipleStrings.Count}");
            multipleSwitch.Transformer(instance, multipleStrings.ToArray());
            i += multipleStrings.Count;
            return i;
        }

        /// <summary>
        ///     Extracts the positional.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="positionalCounts">The positional counts.</param>
        /// <param name="i">The i.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="MissingValueException"></exception>
        internal int ExtractPositional(TOptions instance, string[] args,
            Dictionary<PositionalValues<TOptions>, int> positionalCounts, int i)
        {
            var positional = Order.OfType<PositionalValues<TOptions>>()
                .FirstOrDefault(x => !positionalCounts.ContainsKey(x));
            if (positional == null)
                return i;
            var newValues = positional.Max == null
                ? args.Skip(i).TakeWhile(x => !Switches.ContainsKey(x) && !IsGroupOfBoolean(x)).ToArray()
                : args.Skip(i).TakeWhile(x => !Switches.ContainsKey(x) && !IsGroupOfBoolean(x))
                    .Take(positional.Max.Value).ToArray();

            if (positional.Min.HasValue && newValues.Length < positional.Min.Value)
            {
                throw new MissingValueException($"Positional expected at least {positional.Min.Value} values, but found only {newValues.Length} values");
            }


            positional.Transformer(instance, newValues);
            positionalCounts.Add(positional, newValues.Length);
            i += newValues.Length - 1;
            return i;
        }

        /// <summary>
        ///     Extracts the greedy multiple switch.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="i">The i.</param>
        /// <param name="multipleSwitch">The multiple switch.</param>
        /// <returns>System.Int32.</returns>
        private int ExtractGreedyMultipleSwitch(TOptions instance, string[] args, int i,
            MultipleSwitch<TOptions> multipleSwitch)
        {
            var rest = args.Skip(i + 1).TakeWhile(a => !Switches.ContainsKey(a) && !IsGroupOfBoolean(a)).ToArray();
            multipleSwitch.Transformer(instance, rest);
            i += rest.Length;
            return i;
        }

        /// <summary>
        ///     Extracts the single switch.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="i">The i.</param>
        /// <param name="arg">The argument.</param>
        /// <param name="singleSwitch">The single switch.</param>
        /// <exception cref="MissingValueException">
        /// </exception>
        private void ExtractSingleSwitch(TOptions instance, string[] args, int i, string arg,
            SingleSwitch<TOptions> singleSwitch)
        {
            if (i + 1 >= args.Length)
                throw new MissingValueException($"Switch {arg} requires a value, but none was found.");
            var nextArg = args[i + 1];
            if (Switches.ContainsKey(nextArg) || IsGroupOfBoolean(nextArg))
                throw new MissingValueException($"Switch {arg} requires a value, but found another switch: {nextArg}");
            singleSwitch.Transformer(instance, nextArg);
        }

        /// <summary>
        ///     Determines whether [is group of boolean] [the specified argument].
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <returns><c>true</c> if [is group of boolean] [the specified argument]; otherwise, <c>false</c>.</returns>
        private bool IsGroupOfBoolean(string arg)
        {
            return arg.StartsWith("-") && arg.ToCharArray().Skip(1).Distinct().All(l =>
                       Switches.ContainsKey($"-{l}") && Switches[$"-{l}"] is BooleanSwitch<TOptions>);
        }

        /// <summary>
        ///     Gets or sets the order.
        /// </summary>
        /// <value>The order.</value>
        internal IList<object> Order { get; set; } = new List<object>();

        internal IList<Action<TOptions, IList<string>>> ValidationMethods { get; set; } =
            new List<Action<TOptions, IList<string>>>();
    }
}