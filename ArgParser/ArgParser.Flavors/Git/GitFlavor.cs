using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Flavors.Git
{
    public interface IGitFlavorVisitor
    {
        void Visit(GitFlavor gitFlavor);
    }

    public class AncestorAndDescendentVisitor : IGitFlavorVisitor
    {
        public IList<GitFlavor> GitFlavors { get; set; } = new List<GitFlavor>();

        /// <inheritdoc />
        public void Visit(GitFlavor gitFlavor)
        {
            GitFlavor itr = gitFlavor.BaseFlavor;
            while (itr != null)
            {
                GitFlavors.Add(itr);
                itr = itr.BaseFlavor;
            }

            var queue = new Queue<GitFlavor>();
            queue.Enqueue(gitFlavor);

            while (queue.Any())
            {
                var first = queue.Dequeue();
                GitFlavors.Insert(0, first);
                foreach (var sc in first.SubCommands)
                {
                    queue.Enqueue(sc.Value);
                }
            }

            GitFlavors = GitFlavors.OrderByDescending(f => f.Depth).ToList();
        }
    }

    [DebuggerDisplay("{Name}")]
    public class GitFlavor
    {
        public void Accept(IGitFlavorVisitor visitor)
        {
            visitor.Visit(this);
        }

        public int Depth { get; set; }

        /// <inheritdoc />
        public GitFlavor()
        {
            Parser = new GitParser(this);
        }

        public void AddBooleanSwitch(char letter, string word, Action<object> consume)
        {
            var booleanSwitch = new BooleanSwitch
            {
                Letter = letter,
                Word = word,
                ConsumeCallback = consume
            };
            Switches.Add(booleanSwitch);
            Parser.AddParameter(booleanSwitch);
        }

        public void AddFactoryMethods(params Func<object>[] methods)
        {
            FactoryFunctions.AddRange(methods);
        }

        public void AddPositional(Action<object, string> consume)
        {
            var positional = new Positional
            {
                ConsumeCallback = (o, strings) => consume(o, strings.First()),
                Max = 1,
                Min = 1
            };
            Positionals.Add(positional);
            Parser.AddParameter(positional);
        }

        public void AddPositionals(Action<object, string[]> consume, int min = 1, int max = int.MaxValue)
        {
            var positional = new Positional
            {
                ConsumeCallback = consume,
                Max = max,
                Min = min
            };
            Positionals.Add(positional);
            Parser.AddParameter(positional);
        }

        public void AddSingleValueSwitch(char letter, string word, Action<object, string> consume)
        {
            var singleValueSwitch = new SingleValueSwitch
            {
                ConsumeCallback = consume,
                Letter = letter,
                Word = word
            };
            Switches.Add(singleValueSwitch);
            Parser.AddParameter(singleValueSwitch);
        }

        public void AddSubCommand(string command, GitFlavor flavor)
        {
            flavor.BaseFlavor = this;
            flavor.Depth = Depth + 1;
            SubCommands.Add(command, flavor);
        }

        public void AddValueSwitch(char letter, string word, Action<object, string[]> consume)
        {
            var valuesSwitch = new ValuesSwitch
            {
                Letter = letter,
                Word = word,
                ConsumeCallback = consume
            };
            Switches.Add(valuesSwitch);
            Parser.AddParameter(valuesSwitch);
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
            var visitor = new AncestorAndDescendentVisitor();
            Accept(visitor);
            return strat.Parse(visitor.GitFlavors.Select(x => x.Parser), args);
        }

        public GitFlavor BaseFlavor { get; set; }
        public List<Func<object>> FactoryFunctions { get; set; } = new List<Func<object>>();
        public GitParser Parser { get; set; }
        public List<Positional> Positionals { get; set; } = new List<Positional>();
        public Dictionary<string, GitFlavor> SubCommands { get; set; } = new Dictionary<string, GitFlavor>();
        public List<Switch> Switches { get; set; } = new List<Switch>();
        public string Name { get; set; }
    }
}