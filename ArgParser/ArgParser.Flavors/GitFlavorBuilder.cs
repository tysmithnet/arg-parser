using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ArgParser.Core;
using ArgParser.Core.Help;

namespace ArgParser.Flavors
{
    public class GitParameter : DefaultParameter
    {
        public char? Letter { get; set; }
        public string Word { get; set; }
        public bool IsGroupable { get; set; }
        public bool UsesEqualSignSeparator { get; set; }
        public bool IsPositional { get; set; }
        public int NumberOfValues { get; set; }
        public List<string> Values { get; set; } = new List<string>();

        /// <inheritdoc />
        public GitParameter(Func<object, IIterationInfo, bool> canConsumeCallback = null,
            Func<object, IIterationInfo, IIterationInfo> consumeCallback = null) : base((o, info) => true, (o, info) => info)
        {
            bool isLetter = false;
            bool isWordNoEqual = false;
            bool isWordEquals = false;
            CanConsumeCallback = canConsumeCallback ?? ((o, info) =>
            {
                isLetter = Letter.HasValue || info.Current.Raw == $"-{Letter}";
                isWordNoEqual = !UsesEqualSignSeparator && !Word.IsNullOrWhiteSpace() && info.Current.Raw == $"--{Word}";
                isWordEquals = UsesEqualSignSeparator && !Word.IsNullOrWhiteSpace() &&
                               info.Current.Raw.StartsWith($"--{Word}=");
                return isLetter || isWordEquals || isWordNoEqual;
            });
            ConsumeCallback = consumeCallback ?? ((o, info) =>
            {
                if(NumberOfValues > info.Rest.Count())
                    throw new ArgumentException($"Expected {NumberOfValues} values but found only {info.Rest.Count()}");
                if (isLetter || isWordNoEqual)
                {
                    var raws = info.Rest.Select(x => x.Raw).ToList();
                    Values.AddRange(raws);
                    return info.Consume(1 + raws.Count());
                }

                if (isWordEquals)
                {
                    var rest = info.Current.Raw.Substring($"--{Word}=".Length);
                    Values.Add(rest);
                    return info.Consume(1);
                }

                if (IsPositional)
                {
                    var positionals = info.Rest.Select(x => x.Raw).Take(NumberOfValues - 1).ToList();
                    positionals.Insert(0, info.Current.Raw);
                    Values.AddRange(positionals);
                    return info.Consume(positionals.Count);
                }
                return info;
            });
        }
    }

    public class GitFlavor<T> : IFlavor
    {
        private GitParameter letter;
        private GitParameter wordNoEquals;
        private GitParameter wordEquals;
        private GitParameter positional;
        public IList<GitParameter> Parameters { get; set; }
        public Func<T, IIterationInfo, bool> CanConsumeCallback { get; set; }
        public Func<T, IIterationInfo, IIterationInfo> ConsumeCallback { get; set; }

        /// <inheritdoc />
        public IEnumerable<IToken> Lex(string[] args)
        {
            return args.SelectMany(s =>
            {
                var match = Regex.Match(s, "^-(?<letters>[a-zA-Z0-9]{2,}$)");
                if (!match.Success) return new[] { new DefaultToken(s) };
                var letters = match.Groups["letters"].Value.ToCharArray();
                if (!letters.All(c => Parameters?.Any(p => p.IsGroupable && p.Letter == c) ?? false))
                    return new[] { new DefaultToken(s) };
                return letters.Select(c => new DefaultToken($"-{c}"));
            });
        }

        /// <inheritdoc />
        public IGenericHelp Help { get; }

        public ISet<GitParameter> History { get; set; } = new HashSet<GitParameter>();

        /// <inheritdoc />
        public bool CanConsume(object instance, IIterationInfo info)
        {
            letter = Parameters?.FirstOrDefault(p => $"-{p.Letter}" == info.Current.Raw);
            wordNoEquals = Parameters?.FirstOrDefault(p => p.Word != null && !p.UsesEqualSignSeparator && $"--{p.Word}" == info.Current.Raw);
            wordEquals = Parameters?.FirstOrDefault(p =>
                                p.Word != null && p.UsesEqualSignSeparator && info.Current.Raw.StartsWith($"--{p.Word}="));
            positional = Parameters?.FirstOrDefault(p => p.IsPositional && !History.Contains(p));
            return letter != null || wordNoEquals != null || wordEquals != null || positional != null; 
        }

        /// <inheritdoc />
        public IIterationInfo Consume(object instance, IIterationInfo info)
        {
            if (letter != null)
            {
                
            }

            return info;
        }

        /// <inheritdoc />
        public IParseResult Parse(IEnumerable<IParser> parsers, string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
