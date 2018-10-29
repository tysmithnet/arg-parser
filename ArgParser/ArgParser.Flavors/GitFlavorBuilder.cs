using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ArgParser.Core;
using ArgParser.Core.Help;

namespace ArgParser.Flavors
{

    public class GitToken : IToken
    {
        /// <inheritdoc />
        public string Raw { get; set; }

        public bool IsSubCommand { get; set; }
        public bool IsSwitch { get; set; }
    }

    public class ValueSwitch
    {
        public char Letter { get; set; }
        public string Word { get; set; }
    }

    public class ValuesSwitch : ValueSwitch
    {
        public int Min { get; set; }
        public int Max { get; set; }
    }
    
    public class Positional
    {
        public int Min { get; set; }
        public int Max { get; set; }
    }

    public class GitFlavor<T> : IParser<T>, IParseStrategy<T>, ILexer<GitToken>
    {
        public List<ValueSwitch> BooleanSwitches { get; set; } = new List<ValueSwitch>();
        public List<ValueSwitch> ValueSwitches { get; set; } = new List<ValueSwitch>();
        public List<ValuesSwitch> ValuesSwitches { get; set; } = new List<ValuesSwitch>();
        public List<Positional> Positionals { get; set; } = new List<Positional>();
        public DefaultParser<T> DefaultParser = new DefaultParser<T>();
        public DefaultParseStrategy<T> ParseStrat = new DefaultParseStrategy<T>();
        public List<Func<T>> FactoryMethods { get; set; } = new List<Func<T>>();

        public IParseResult Parse(string[] args)
        {
            
        }

        public void AddFactoryMethods(params Func<T>[] funcs)
        {
            FactoryMethods.AddRange(funcs);
        }

        public void AddBooleanParameter(char letter, string word, Action<T> consume)
        {
            BooleanSwitches.Add(new ValueSwitch()
            {
                Letter = letter,
                Word = word
            });
            bool CanConsume(T inst, IIterationInfo info) => info.Current.Raw == $"-{letter}" || info.Current.Raw == $"--{word}";

            IIterationInfo DoConsume(T inst, IIterationInfo info)
            {
                consume(inst);
                return info.Consume(1);
            }

            DefaultParser.AddParameter(new DefaultParameter<T>(CanConsume, DoConsume));
        }

        public void AddValueSwitch(char letter, string word, Action<T, string> consume)
        {
            ValueSwitches.Add(new ValueSwitch()
            {
                Letter = letter,
                Word = word
            });

            bool CanConsume(T inst, IIterationInfo info) => info.Current.Raw == $"-{letter}" || info.Current.Raw == $"--{word}";

            IIterationInfo DoConsume(T inst, IIterationInfo info)
            {
                if(info.Next == null)
                    throw new InvalidOperationException(); // todo
                consume(inst, info.Next.Raw);
                return info.Consume(2);
            }

            DefaultParser.AddParameter(new DefaultParameter<T>(CanConsume, DoConsume));
        }

        public void AddValuesStitch(char letter, string word, Action<T, string[]> consume, int min = 1, int max = int.MaxValue)
        {
            ValuesSwitches.Add(new ValuesSwitch()
            {
                Letter = letter,
                Word = word,

            });

            bool CanConsume(T inst, IIterationInfo info) => info.Current.Raw == $"-{letter}" || info.Current.Raw == $"--{word}";

            IIterationInfo DoConsume(T inst, IIterationInfo info)
            {
                return info.Consume(1); // todo: nonsense
            }

            DefaultParser.AddParameter(new DefaultParameter<T>(CanConsume, DoConsume));
        }

        public void AddSubCommand<TSub>(string word, GitFlavor<TSub> flavor) where TSub : T
        {
            flavor.BaseParser = this;
        }

        public ISet<string> SubCommands { get; set; } = new HashSet<string>();


        /// <inheritdoc />
        public IGenericHelp Help => DefaultParser.Help;

        /// <inheritdoc />
        public bool CanConsume(object instance, IIterationInfo info)
        {
            return ((DefaultParser)DefaultParser).CanConsume(instance, info);
        }

        /// <inheritdoc />
        public IIterationInfo Consume(object instance, IIterationInfo info)
        {
            return ((DefaultParser) DefaultParser).Consume(instance, info);
        }

        /// <inheritdoc />
        public IParser BaseParser
        {
            get => DefaultParser.BaseParser;
            set => DefaultParser.BaseParser = value;
        }

        /// <inheritdoc />
        public bool CanConsume<TSub>(TSub instance, IIterationInfo info) where TSub : T
        {
            return DefaultParser.CanConsume(instance, info);
        }

        /// <inheritdoc />
        public IIterationInfo Consume<TSub>(TSub instance, IIterationInfo info) where TSub : T
        {
            return DefaultParser.Consume(instance, info);
        }

        /// <inheritdoc />
        public IParseResult Parse(IEnumerable<IParser<T>> parsers, string[] args)
        {
            return ParseStrat.Parse(parsers, args);
        }

        /// <inheritdoc />
        public IParseResult Parse(IEnumerable<IParser> parsers, string[] args)
        {
            return ParseStrat.Parse(parsers, args);
        }

        /// <inheritdoc />
        IEnumerable<GitToken> ILexer<GitToken>.Lex(string[] args)
        {
            if (args == null || !args.Any())
                yield break;
            var booleanLetters = BooleanSwitches.Select(x => x.Letter.ToString()).Join("");
            var switchLetters = ValueSwitches.Select(x => x.Letter.ToString()).Join("");
            var switchesLetters = ValuesSwitches.Select(x => x.ToString()).Join("");

            var groupRegex = new Regex($@"^-(?<b>[{booleanLetters}])+(?<v>[{switchLetters + switchesLetters}])?$");

            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                if (i == 0 && SubCommands.Contains(arg))
                {
                    yield return new GitToken()
                    {
                        IsSubCommand = true,
                        Raw = arg
                    };
                    continue;
                }

                var firstBool = BooleanSwitches
                    .Concat(ValueSwitches)
                    .Concat(ValuesSwitches)
                    .FirstOrDefault(s => arg == $"-{s.Letter}");

                if (firstBool != null)
                {
                    yield return new GitToken()
                    {
                        Raw = arg,
                        IsSwitch = true,
                    };
                    continue;
                }
                
                var groupMatch = groupRegex.Match(arg);
                if (groupMatch.Success)
                {
                    var booleans = groupMatch.Groups["b"].Value;
                    var other = groupMatch.Groups["v"].Success ? groupMatch.Groups["v"].Value : "";
                    foreach (var c in booleans)
                    {
                        yield return new GitToken()
                        {
                            Raw = $"-{c}",
                            IsSwitch = true
                        };
                    }

                    foreach (var c in other)
                    {
                        yield return new GitToken()
                        {
                            Raw = $"-{c}",
                            IsSwitch = true
                        };
                    }
                    continue;
                }
            }
        }

        /// <inheritdoc />
        IEnumerable<IToken> ILexer.Lex(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}