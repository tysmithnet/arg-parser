using System;
using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Flavors
{
    public class GitFlavor
    {
        public GitParser Parser { get; set; }

        public void AddBooleanSwitch(char letter, string word, Action<object> consume)
        {
            Switches.Add(new BooleanSwitch
            {
                Letter = letter,
                Word = word,
                ConsumeCallback = consume
            });
        }

        public void AddFactoryMethods(params Func<object>[] methods)
        {
            FactoryMethods.AddRange(methods);
        }

        public void AddSingleValueSwitch(char letter, string word, Action<object, string> consume)
        {
            Switches.Add(new SingleValueSwitch
            {
                ConsumeCallback = consume,
                Letter = letter,
                Word = word
            });
        }

        public void AddSubCommand(string command, GitFlavor flavor)
        {
            flavor.BaseFlavor = this;
            SubCommands.Add(command, flavor);
        }

        public void AddValueSwitch(char letter, string word, Action<object, string[]> consume)
        {
            Switches.Add(new ValuesSwitch
            {
                Letter = letter,
                Word = word,
                ConsumeCallback = consume
            });
        }

        /// <inheritdoc />
        public GitFlavor()
        {
            Parser = new GitParser(this);
        }

        public IEnumerable<IParser> GetParsers()
        {
            
            foreach (var @switch in Switches) Parser.AddParameter(@switch);
            foreach (var positional in Positionals) Parser.AddParameter(positional);

            var others = SubCommands.Values.SelectMany(x => x.GetParsers());
            return new[] { Parser }.Concat(others);
        }

        public IParseResult Parse(string[] args)
        {
            var strat = new GitParseStrategy(FactoryMethods);
            return strat.Parse(GetParsers(), args);
        }

        public GitFlavor BaseFlavor { get; set; }
        public List<Func<object>> FactoryMethods { get; set; } = new List<Func<object>>();
        public List<Positional> Positionals { get; set; } = new List<Positional>();
        public Dictionary<string, GitFlavor> SubCommands { get; set; } = new Dictionary<string, GitFlavor>();
        public List<Switch> Switches { get; set; } = new List<Switch>();
    }
}