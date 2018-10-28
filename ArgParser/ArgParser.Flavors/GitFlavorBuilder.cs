using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ArgParser.Core;
using ArgParser.Core.Help;

namespace ArgParser.Flavors
{
    public class Switch<T>
    {
        public bool IsGroupable { get; set; }
        public char? Letter { get; set; }
        public string Word { get; set; }
    }

    public class ValueSwitch<T> : Switch<T>
    {
        public Action<T, string> Consume { get; set; }
        public Regex Regex { get; set; }
        public string Separator { get; set; }
    }

    public class ValuesSwitch<T> : Switch<T>
    {
        public Action<T, string[]> Consume { get; set; }
    }

    public class Positional<T>
    {
        public Action<T, string> Consume { get; set; }
    }

    public class PositionalList<T>
    {
        public Action<T, string[]> Consume { get; set; }
        public int? Max { get; set; }
        public int? Min { get; set; }
    }

    public class BooleanSwitch<T> : Switch<T>
    {
        public Action<T> Consume { get; set; }
    }

    public class GitFlavor<T> : IParser<T>, IFlavor, IEnumerable<IParser>
    {
        public GitFlavor(Func<T> factoryFunc)
        {
            FactoryFunc = factoryFunc;
            DefaultParser = new DefaultParser<T>();
            _defaultParseStrategy = new DefaultParseStrategy<T>();
        }

        private DefaultParseStrategy<T> _defaultParseStrategy;

        public void AddBooleanSwitch(char letter, string word, Action<T> consume)
        {
            var s = new BooleanSwitch<T>
            {
                Letter = letter,
                Word = word,
                Consume = consume
            };
            BooleanSwitches.Add(s);
        }

        public void AddPositional(Action<T, string> consume)
        {
            var p = new Positional<T>
            {
                Consume = consume
            };
            Positionals.Add(p);
        }

        public void AddPositionalList(Action<T, string[]> consume, int? min = 1, int? max = 1)
        {
            var p = new PositionalList<T>
            {
                Consume = consume,
                Max = max,
                Min = min
            };
            PositionalLists.Add(p);
        }

        public void AddSubCommand<TSub>(string subCommand, GitFlavor<TSub> parser) where TSub : T
        {
            parser.BaseParser = this;
            SubCommands.Add(subCommand, parser);
        }

        public void AddValueSwitch(char letter, string word, Action<T, string> consume, string separator = null,
            Regex regex = null)
        {
            var s = new ValueSwitch<T>
            {
                Letter = letter,
                Word = word,
                Consume = consume,
                Separator = separator,
                Regex = regex
            };
            ValueSwitches.Add(s);
        }

        /// <inheritdoc />
        public bool CanConsume(object instance, IIterationInfo info) =>
            ((DefaultParser) DefaultParser).CanConsume(instance, info);

        /// <inheritdoc />
        public bool CanConsume<TSub>(TSub instance, IIterationInfo info) where TSub : T =>
            DefaultParser.CanConsume(instance, info);

        /// <inheritdoc />
        public IIterationInfo Consume(object instance, IIterationInfo info) =>
            ((DefaultParser) DefaultParser).Consume(instance, info);

        /// <inheritdoc />
        public IIterationInfo Consume<TSub>(TSub instance, IIterationInfo info) where TSub : T =>
            DefaultParser.Consume(instance, info);

        /// <inheritdoc />
        public IEnumerator<IParser> GetEnumerator()
        {
            IParser itr = this;
            while (itr != null)
            {
                yield return itr;
                itr = itr.BaseParser;
            }
        }

        /// <inheritdoc />
        public IParseResult Parse(string[] args)
        {
            _defaultParseStrategy = new DefaultParseStrategy<T>(FactoryFuncs);
            return _defaultParseStrategy.Parse(this, args);
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public IParser BaseParser
        {
            get => DefaultParser.BaseParser;
            set => DefaultParser.BaseParser = value;
        }

        public IList<BooleanSwitch<T>> BooleanSwitches { get; set; } = new List<BooleanSwitch<T>>();
        public DefaultParser<T> DefaultParser { get; set; }
        public Func<T> FactoryFunc { get; set; }
        public List<Func<T>> FactoryFuncs { get; set; } = new List<Func<T>>();
        public IGenericHelp Help => DefaultParser.Help;
        public IList<PositionalList<T>> PositionalLists { get; set; } = new List<PositionalList<T>>();
        public IList<Positional<T>> Positionals { get; set; } = new List<Positional<T>>();
        public Dictionary<string, IParser> SubCommands { get; set; } = new Dictionary<string, IParser>();
        public IList<ValuesSwitch<T>> ValuesSwitches { get; set; } = new List<ValuesSwitch<T>>();
        public IList<ValueSwitch<T>> ValueSwitches { get; set; } = new List<ValueSwitch<T>>();
    }
}