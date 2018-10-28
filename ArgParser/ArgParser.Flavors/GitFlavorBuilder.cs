using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ArgParser.Core;
using ArgParser.Core.Help;

namespace ArgParser.Flavors
{
    public class Switch : IParameter
    {
        public char? Letter { get; set; }
        public string Word { get; set; }
        public bool IsGroupable { get; set; }

        /// <inheritdoc />
        public bool CanConsume(object instance, IIterationInfo info)
        {
            if (Letter.HasValue && info.Current.Raw == $"-{Letter.Value}")
                return true;
            if (!Word.IsNullOrWhiteSpace() && info.Current.Raw == $"--{Word}")
                return true;
            return false;
        }

        /// <inheritdoc />
        public virtual IIterationInfo Consume(object instance, IIterationInfo info)
        {
            return info;
        }
    }

    public class BooleanSwitch : Switch
    {
        public Action<object> Callback { get; set; }

        /// <inheritdoc />
        public override IIterationInfo Consume(object instance, IIterationInfo info)
        {
            Callback(instance);
            return info.Consume(1);
        }
    }

    public class BooleanSwitch<T> : BooleanSwitch, IParameter<T>
    {
        /// <inheritdoc />
        public bool CanConsume(T instance, IIterationInfo info)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IIterationInfo Consume(T instance, IIterationInfo info)
        {
            throw new NotImplementedException();
        }
    }

    public class ValueSwitch : Switch
    {
        public Action<object, string> Consume { get; set; }
    }

    public class ValueSwitch<T> : ValueSwitch
    {
    }

    public class ValuesSwitch : Switch, IMultiValue
    {
        public Action<object, string[]> Consume { get; set; }
        public int? Min { get; set; }
        public int? Max { get; set; }
    }

    public class Positional
    {
        public Action<object, string> Consume { get; set; }
    }

    public class PositionalList : IMultiValue
    {
        /// <inheritdoc />
        public Action<object, string[]> Consume { get; set;  }

        /// <inheritdoc />
        public int? Min { get; set; }

        /// <inheritdoc />
        public int? Max { get; set; }
    }

    public interface IMultiValue
    {
        Action<object, string[]> Consume { get; }
        int? Min { get; }
        int? Max { get; }
    }

    public class SubCommand
    {
        public string Name { get; set; }
        public GitFlavor GitFlavor { get; set; }
    }

    public interface IGitFlavorVisitor
    {
        void Visit(GitFlavor flavor);
    }

    public class GitFlavor : IFlavor
    {
        public Func<object> InstanceFactory { get; set; }
        public GitFlavor BaseFlavor { get; set; }
        public List<BooleanSwitch> BooleanSwitches { get; set; } = new List<BooleanSwitch>();
        public List<ValueSwitch> ValueSwitches { get; set; } = new List<ValueSwitch>();
        public List<ValuesSwitch> ValuesSwitches { get; set; } = new List<ValuesSwitch>();
        public List<Positional> Positionals { get; set; } = new List<Positional>();
        public List<PositionalList> PositionalLists { get; set; } = new List<PositionalList>();
        public List<SubCommand> SubCommands { get; set; } = new List<SubCommand>();
        public DefaultParser DefaultParser { get; set; } = new DefaultParser();

        public void Accept(IGitFlavorVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void AddSubCommand(string command, GitFlavor flavor)
        {
            flavor.BaseFlavor = this;
            SubCommands.Add(new SubCommand()
            {
                Name = command,
                GitFlavor = flavor
            });
        }

        public void AddBooleanSwitch(char letter, string word, Action<object> consume)
        {
            BooleanSwitches.Add(new BooleanSwitch()
            {
                Letter = letter,
                Word = word,
                Consume = consume
            });
        }

        public void AddValueSwitch(char letter, string word, Action<object, string> consume)
        {
            ValueSwitches.Add(new ValueSwitch()
            {
                Letter = letter,
                Word = word,
                Consume = consume
            });
        }

        public void AddValuesSwitch(char letter, string word, Action<object, string[]> consume, int? min = 1, int? max = int.MaxValue)
        {
            ValuesSwitches.Add(new ValuesSwitch()
            {
                Letter = letter,
                Word = word,
                Consume = consume,
                Min = min,
                Max = max
            });
        }

        public void AddPositional(Action<object, string> consume)
        {
            Positionals.Add(new Positional()
            {
                Consume = consume
            });
        }

        public void AddValuesSwitch(Action<object, string[]> consume, int? min = 1, int? max = int.MaxValue)
        {
            PositionalLists.Add(new PositionalList()
            {
                Consume = consume,
                Min = min,
                Max = max
            });
        }

        /// <inheritdoc />
        public IParseResult Parse(string[] args)
        {
            var parserCollector = new ParserCollector();
            Accept(parserCollector);
            var facCollector = new FactoryMethodCollector();
            Accept(facCollector);
            var strat = new DefaultParseStrategy(facCollector.Funcs);
            return strat.Parse(parserCollector.Parsers, args);
        }
    }

    public class FactoryMethodCollector : IGitFlavorVisitor
    {
        public List<Func<object>> Funcs { get; set; }

        /// <inheritdoc />
        public void Visit(GitFlavor flavor)
        {
            Funcs.Add(flavor.InstanceFactory);
            foreach (var sub in flavor.SubCommands)
            {
                sub.GitFlavor.Accept(this);
            }
        }
    }

    public class ParserCollector : IGitFlavorVisitor
    {
        public List<IParser> Parsers { get; set; } = new List<IParser>();

        /// <inheritdoc />
        public void Visit(GitFlavor flavor)
        {
            Parsers.Add(flavor.DefaultParser);
            foreach (var sub in flavor.SubCommands)
            {
                sub.GitFlavor.Accept(this);
            }
        }
    }
}