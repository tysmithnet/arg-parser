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
        private bool _isLetter;
        private bool _isWordNoEqual;
        private bool _isWordEquals;
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
            _isLetter = false;
            _isWordNoEqual = false;
            _isWordEquals = false;
            CanConsumeCallback = canConsumeCallback ?? ((o, info) =>
            {
                ExtractBooleanResults(info);
                return base.CanConsume(o, info) || _isLetter || _isWordEquals || _isWordNoEqual;
            });
            ConsumeCallback = consumeCallback ?? ((o, info) =>
            {
                ExtractBooleanResults(info);
                if (NumberOfValues > info.Rest.Count())
                    throw new ArgumentException($"Expected {NumberOfValues} values but found only {info.Rest.Count()}");
                if (_isLetter || _isWordNoEqual)
                {
                    var raws = info.Rest.Take(NumberOfValues).Select(x => x.Raw).ToList();
                    Values.AddRange(raws);
                    return info.Consume(1 + raws.Count());
                }

                if (_isWordEquals)
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

        private void ExtractBooleanResults(IIterationInfo info)
        {
            _isLetter = Letter.HasValue || info.Current.Raw == $"-{Letter}";
            _isWordNoEqual = !UsesEqualSignSeparator && !Word.IsNullOrWhiteSpace() && info.Current.Raw == $"--{Word}";
            _isWordEquals = UsesEqualSignSeparator && !Word.IsNullOrWhiteSpace() &&
                            info.Current.Raw.StartsWith($"--{Word}=");
        }
    }

    public class GitParameter<T> : GitParameter
    {
        public GitParameter(Func<object, IIterationInfo, bool> canConsumeCallback = null, Func<object, IIterationInfo, IIterationInfo> consumeCallback = null) : base(canConsumeCallback, consumeCallback)
        {
        }
    }

    public class GitFlavor<T> : IFlavor<T>
    {
        private GitParameter letter;
        private GitParameter wordNoEquals;
        private GitParameter wordEquals;
        private GitParameter positional;
        private DefaultParseStrategy<T> parseStrategy;
        public IList<GitParameter> Parameters { get; set; } = new List<GitParameter>();
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

        public GitFlavor(IEnumerable<Func<T>> factoryFuncs)
        {
            FactoryFuncs = factoryFuncs.ToList();
            parseStrategy = new DefaultParseStrategy<T>(FactoryFuncs);
        }

        public List<Func<T>> FactoryFuncs { get; set; }

        /// <inheritdoc />
        public IGenericHelp Help { get; set; }

        public ISet<GitParameter> History { get; set; } = new HashSet<GitParameter>();

        /// <inheritdoc />
        public bool CanConsume(object instance, IIterationInfo info)
        {
            if (instance is T casted)
                return CanConsume(casted, info);
            return false;
        }

        /// <inheritdoc />
        public IIterationInfo Consume(object instance, IIterationInfo info)
        {
            if (instance is T casted)
                return Consume(casted, info);
            return info;
        }

        /// <inheritdoc />
        public IParseResult Parse(IEnumerable<IParser<T>> parsers, string[] args)
        {
            return parseStrategy.Parse(new[] {this}, args);
        }

        /// <inheritdoc />
        public IParseResult Parse(IEnumerable<IParser> parsers, string[] args)
        {
            return parseStrategy.Parse(new[] {this}, args);
        }

        /// <inheritdoc />
        public IParseResult Parse(string[] args)
        {
            return Parse(new[] {this}, args);
        }

        /// <inheritdoc />
        public bool CanConsume<TSub>(TSub instance, IIterationInfo info) where TSub : T
        {
            ExtractBooleanValues<TSub>(info);
            return letter != null || wordNoEquals != null || wordEquals != null || positional != null;
        }

        private void ExtractBooleanValues<TSub>(IIterationInfo info) where TSub : T
        {
            letter = Parameters?.FirstOrDefault(p => $"-{p.Letter}" == info.Current.Raw);
            wordNoEquals = Parameters?.FirstOrDefault(p =>
                p.Word != null && !p.UsesEqualSignSeparator && $"--{p.Word}" == info.Current.Raw);
            wordEquals = Parameters?.FirstOrDefault(p =>
                p.Word != null && p.UsesEqualSignSeparator && info.Current.Raw.StartsWith($"--{p.Word}="));
            positional = Parameters?.FirstOrDefault(p => p.IsPositional && !History.Contains(p));
        }

        /// <inheritdoc />
        public IIterationInfo Consume<TSub>(TSub instance, IIterationInfo info) where TSub : T
        {
            ExtractBooleanValues<TSub>(info);
            var parameter = letter ?? wordNoEquals ?? wordEquals ?? positional;
            if (parameter != null)
                return parameter.Consume(instance, info);

            return info;
        }

        /// <inheritdoc />
        public IParser BaseParser { get; set; }
    }
}
