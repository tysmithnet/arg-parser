using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ArgParser.Core;
using ArgParser.Core.Help;

namespace ArgParser.Flavors.Git
{
    [DebuggerDisplay("{Name}")]
    public class GitParser : IParser
    {
        public GitParser(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
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
            if (required)
                RequiredParameters.Add(singleValueSwitch);
            if (isGroupable)
                GroupableSwitches.Add(singleValueSwitch);
        }

        public void AddSubCommand(string command, GitParser parser)
        {
            parser.BaseFlavor = this;
            parser.Depth = Depth + 1;
            SubCommands.Add(command, parser);
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

            var ancestors = GitFlavorRepository.GetAncestors(Name).ToList();
            ancestors.Insert(0, this);
            var children = GitFlavorRepository.GetChildren(Name, true);
            return strat.Parse(ancestors.Concat(children), args);
        }

        private readonly List<IParameter> _allParameters = new List<IParameter>();

        public virtual void AddParameter(IParameter parameter, IGenericHelp help = null)
        {
            _allParameters.Add(parameter);
            DefaultParser.AddParameter(parameter, help);
        }

        public bool CanConsume(object instance, IIterationInfo info)
        {
            var canSelf = DefaultParser.CanConsume(instance, info);
            var canBase = GitFlavorRepository.GetParent(Name)?.CanConsume(instance, info) ?? false;
            return canSelf || canBase;
        }

        public IIterationInfo Consume(object instance, IIterationInfo info)
        {
            var canSelf = DefaultParser.CanConsume(instance, info);
            if (canSelf)
                return DefaultParser.Consume(instance, info);
            var ancestors = GitFlavorRepository.GetAncestors(Name);
            foreach (var gitFlavor in ancestors)
                if (gitFlavor.CanConsume(instance, info))
                    return gitFlavor.Consume(instance, info);
            throw new InvalidOperationException(
                $"Consume was called on {Name}, but it, nor its ancestors are able to consume. Was CanConsume called before this invocation?");
        }

        public void Reset()
        {
            foreach (var allParameter in _allParameters) allParameter.Reset();
        }

        public IParser BaseParser => GitFlavorRepository.GetParent(Name);
        public DefaultParser DefaultParser { get; set; } = new DefaultParser();

        public IGitFlavorRepository GitFlavorRepository { get; set; }

        public IGenericHelp Help => DefaultParser.Help;
        public string Name { get; set; }

        public int Depth { get; set; }
        public List<Func<object>> FactoryFunctions { get; set; } = new List<Func<object>>();
        public IGitValidatorRepository GitValidatorRepository { get; set; }

        public List<Switch> GroupableSwitches { get; set; } = new List<Switch>();
        public List<Positional> Positionals { get; set; } = new List<Positional>();
        public IList<GitParameter> RequiredParameters { get; set; } = new List<GitParameter>();
        public Dictionary<string, GitParser> SubCommands { get; set; } = new Dictionary<string, GitParser>();
        public List<Switch> Switches { get; set; } = new List<Switch>();
    }
}