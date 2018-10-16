using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ArgParser.Core
{
    public interface IParser<TOptions> where TOptions : IOptions
    {
        TOptions Parse(string[] args);
        string GetHelp(string question);
    }

    public interface IOptions
    {

    }

    public interface ICommandLineElement
    {
        bool Required { get; }
        string HelpText { get; }
    }

    internal abstract class Switch<TOptions> : ICommandLineElement where TOptions : IOptions
    {
        public char Letter { get; set; }
        public string Word { get; set; }
        public string HelpText { get; set; }
        public bool Required { get; set; }
    }

    internal abstract class ValueSwitch<TOptions> : Switch<TOptions> where TOptions : IOptions
    {
        public Regex Regex { get; set; }

    }

    internal class BooleanSwitch<TOptions> : Switch<TOptions> where TOptions : IOptions
    {
        public Action<TOptions> Transformer { get; set; }
    }

    internal class SingleSwitch<TOptions> : ValueSwitch<TOptions> where TOptions : IOptions
    {
        public Action<TOptions, string> Transformer { get; set; }
    }

    internal class MultipleSwitch<TOptions> : ValueSwitch<TOptions> where TOptions : IOptions
    {
        public int Count { get; set; } = -1;
        public Action<TOptions, string[]> Transformer { get; set; }
    }

    internal class PositionalValues<TOptions> : ICommandLineElement where TOptions : IOptions
    {
        public int Count { get; set; }
        public Regex Regex { get; set; }
        public Action<TOptions, string[]> Transformer { get; set; }

        /// <inheritdoc />
        public bool Required { get; set; }

        /// <inheritdoc />
        public string HelpText { get; set; }
    }

    public class MissingValueException : ArgumentException
    {
        public MissingValueException(string message) : base(message)
        {
        }
    }

    public class UnexpectedArgumentException : ArgumentException
    {
        /// <inheritdoc />
        public UnexpectedArgumentException(string message) : base(message)
        {
        }
    }

    public class OptionsBuilder<TOptions> where TOptions : IOptions
    {
        internal IList<object> Order { get; set; } = new List<object>();
        internal Dictionary<string, Switch<TOptions>> Switches = new Dictionary<string, Switch<TOptions>>();
        internal IList<PositionalValues<TOptions>> Positionals = new List<PositionalValues<TOptions>>();
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
