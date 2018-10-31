using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Flavors.Git
{
    [DebuggerDisplay("{Name}")]
    public class GitFlavor
    {
        public GitFlavor()
        {
            Parser = new GitParser(this);
        }

        public void Accept(IGitFlavorVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void AddBooleanSwitch(char letter, string word, Action<object> consume, bool required = false,
            bool isGroupable = false)
        {
            var booleanSwitch = new BooleanSwitch
            {
                Letter = letter,
                Word = word,
                ConsumeCallback = consume
            };
            Switches.Add(booleanSwitch);
            Parser.AddParameter(booleanSwitch);
            if (required)
                RequiredParameters.Add(booleanSwitch);
            if (isGroupable)
                GroupableSwitches.Add(booleanSwitch);
        }

        public void AddFactoryMethods(params Func<object>[] methods)
        {
            FactoryFunctions.AddRange(methods);
        }

        public void AddPositional(Action<object, string> consume, bool required = false)
        {
            var positional = new Positional
            {
                ConsumeCallback = (o, strings) => consume(o, strings.First()),
                Max = 1,
                Min = 1
            };
            Positionals.Add(positional);
            Parser.AddParameter(positional);
            if (required)
                RequiredParameters.Add(positional);
        }

        public void AddPositionals(Action<object, string[]> consume, int min = 1, int max = int.MaxValue,
            bool required = false)
        {
            var positional = new Positional
            {
                ConsumeCallback = consume,
                Max = max,
                Min = min
            };
            Positionals.Add(positional);
            Parser.AddParameter(positional);
            if (required)
                RequiredParameters.Add(positional);
        }

        public void AddSingleValueSwitch(char letter, string word, Action<object, string> consume,
            bool required = false, bool isGroupable = false)
        {
            var singleValueSwitch = new SingleValueSwitch
            {
                ConsumeCallback = consume,
                Letter = letter,
                Word = word
            };
            Switches.Add(singleValueSwitch);
            Parser.AddParameter(singleValueSwitch);
            if (required)
                RequiredParameters.Add(singleValueSwitch);
            if (isGroupable)
                GroupableSwitches.Add(singleValueSwitch);
        }

        public void AddSubCommand(string command, GitFlavor flavor)
        {
            flavor.BaseFlavor = this;
            flavor.Depth = Depth + 1;
            SubCommands.Add(command, flavor);
        }

        public void AddValueSwitch(char letter, string word, Action<object, string[]> consume, bool required = false,
            bool isGroupable = false)
        {
            var valuesSwitch = new ValuesSwitch
            {
                Letter = letter,
                Word = word,
                ConsumeCallback = consume
            };
            Switches.Add(valuesSwitch);
            Parser.AddParameter(valuesSwitch);
            if (required)
                RequiredParameters.Add(valuesSwitch);
            if (isGroupable)
                GroupableSwitches.Add(valuesSwitch);
        }

        public IParseResult Parse(string[] args, IEnumerable<Func<object>> factoryFunctions = null)
        {
            var possibleSubCommand = args[0];
            if (SubCommands.ContainsKey(possibleSubCommand))
            {
                var subCommandFlavor = SubCommands[possibleSubCommand];
                return subCommandFlavor.Parse(args.Skip(1).ToArray(), factoryFunctions ?? FactoryFunctions);
            }

            var strat = new GitParseStrategy(factoryFunctions ?? FactoryFunctions);
            if (RequiredParameters.Any())
            {
                var validators = RequiredParameters.Select(parameter => new RequiredParameterValidator(parameter));
                foreach (var requiredParameterValidator in validators) strat.Validators.Add(requiredParameterValidator);
            }

            var visitor = new AncestorAndDescendentVisitor();
            Accept(visitor);
            return strat.Parse(visitor.GitFlavors.Select(x => x.Parser), args);
        }

        public GitFlavor BaseFlavor { get; set; }
        public int Depth { get; set; }
        public List<Func<object>> FactoryFunctions { get; set; } = new List<Func<object>>();

        public List<Switch> GroupableSwitches { get; set; } = new List<Switch>();
        public string Name { get; set; }
        public GitParser Parser { get; set; }
        public List<Positional> Positionals { get; set; } = new List<Positional>();
        public IList<GitParameter> RequiredParameters { get; set; } = new List<GitParameter>();
        public Dictionary<string, GitFlavor> SubCommands { get; set; } = new Dictionary<string, GitFlavor>();
        public List<Switch> Switches { get; set; } = new List<Switch>();
    }
}