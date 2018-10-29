﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ArgParser.Core;
using ArgParser.Core.Help;

namespace ArgParser.Flavors
{
    public static class TokenExtensions
    {
        public static GitToken ToGitToken(this IToken token)
        {
            if (token is GitToken casted)
                return casted;
            return new GitToken
            {
                Raw = token.Raw,
                WordMatch = Regex.Match(token.Raw, "^--(?<k>[^-]+)$"),
                LetterMatch = Regex.Match(token.Raw, "^-(?<k>[^-])$"),
                GroupMatch = Regex.Match(token.Raw, @"^-(?<k>\S+)$"),
                WordEqualMatch = Regex.Match(token.Raw, @"^(?<k>--[^-]+)=(?<v>\S+)$")
            };
        }
    }

    public class GitToken : IToken
    {
        public Match GroupMatch { get; set; }

        public bool IsAnyMatch =>
            WordMatch.Success || LetterMatch.Success || WordEqualMatch.Success || GroupMatch.Success;

        public string Key => (WordEqualMatch?.Success ?? false) ? WordMatch?.Groups["k"].Value : null;
        public char? Letter => (LetterMatch?.Success ?? false) ? LetterMatch?.Groups["k"].Value[0] : null;
        public Match LetterMatch { get; set; }
        public int Order { get; set; }

        /// <inheritdoc />
        public string Raw { get; set; }

        public string Value => (WordEqualMatch?.Success ?? false) ? WordMatch?.Groups["v"].Value : null;
        public string Word => (WordMatch?.Success ?? false) ? WordMatch?.Groups["k"].Value : null;
        public Match WordEqualMatch { get; set; }
        public Match WordMatch { get; set; }
    }

    public class GitLexer : ILexer
    {
        /// <inheritdoc />
        public IEnumerable<IToken> Lex(string[] args)
        {
            return DefaultLexer.Lex(args).Select(x => x.ToGitToken());
        }

        public DefaultLexer DefaultLexer { get; set; } = new DefaultLexer();
    }

    public abstract class Switch : IParameter
    {
        public virtual bool CanConsume(object instance, IIterationInfo info)
        {
            var cur = info.Current.ToGitToken();
            if (cur.Letter != null && cur.Letter == Letter)
                return true;
            if (cur.Word != null && cur.Word == Word)
                return true;
            return false;
        }

        /// <inheritdoc />
        public abstract IIterationInfo Consume(object instance, IIterationInfo info);

        public char Letter { get; set; }
        public string Word { get; set; }
    }

    public class BooleanSwitch : Switch
    {
        /// <inheritdoc />
        public override IIterationInfo Consume(object instance, IIterationInfo info)
        {
            ConsumeCallback(instance);
            return info.Consume(1);
        }

        public Action<object> ConsumeCallback { get; set; }
    }

    public class SingleValueSwitch : Switch
    {
        /// <inheritdoc />
        public override IIterationInfo Consume(object instance, IIterationInfo info)
        {
            ConsumeCallback(instance, info.Next.Raw); // todo: check
            return info.Consume(2);
        }

        public Action<object, string> ConsumeCallback { get; set; }
    }

    public class ValuesSwitch : Switch
    {
        /// <inheritdoc />
        public override IIterationInfo Consume(object instance, IIterationInfo info)
        {
            var tokens = info.Rest.Select(x => x.ToGitToken()).TakeWhile(t => !t.IsAnyMatch).Select(t => t.Raw)
                .ToArray();
            // todo: check count
            ConsumeCallback(instance, tokens);
            return info.Consume(1 + tokens.Length);
        }

        public Action<object, string[]> ConsumeCallback { get; set; }
    }

    public class Positional : IParameter
    {
        /// <inheritdoc />
        public bool CanConsume(object instance, IIterationInfo info)
        {
            var ar = info.Rest.Select(x => x.ToGitToken()).TakeWhile(t => !t.IsAnyMatch).ToArray();
            return ar.Length >= Min && ar.Length < Max;
        }

        /// <inheritdoc />
        public IIterationInfo Consume(object instance, IIterationInfo info)
        {
            var tokens = info.Rest.Select(x => x.ToGitToken()).TakeWhile(t => !t.IsAnyMatch).Select(t => t.Raw)
                .ToArray();
            // todo: check count
            ConsumeCallback(instance, tokens);
            return info.Consume(1 + tokens.Length);
        }

        public Action<object, string[]> ConsumeCallback { get; set; }
        public int Max { get; set; } = int.MaxValue;
        public int Min { get; set; } = 1;
    }

    public class GitParser : IParser
    {
        /// <inheritdoc />
        public GitParser(GitFlavor flavor)
        {
            _flavor = flavor ?? throw new ArgumentNullException(nameof(flavor));
        }

        private readonly GitFlavor _flavor;

        public virtual void AddParameter(IParameter parameter, IGenericHelp help = null)
        {
            DefaultParser.AddParameter(parameter, help);
        }

        /// <inheritdoc />
        public bool CanConsume(object instance, IIterationInfo info)
        {
            if (info.Index == 0 && _flavor.SubCommands.ContainsKey(info.Current.Raw)) return true;

            return DefaultParser.CanConsume(instance, info);
        }

        /// <inheritdoc />
        public IIterationInfo Consume(object instance, IIterationInfo info)
        {
            if (info.Index == 0 && _flavor.SubCommands.ContainsKey(info.Current.Raw)) return info.Consume(1);
            return DefaultParser.Consume(instance, info);
        }

        /// <inheritdoc />
        public IParser BaseParser
        {
            get => DefaultParser.BaseParser;
            set => DefaultParser.BaseParser = value;
        }

        public DefaultParser DefaultParser { get; set; } = new DefaultParser();

        /// <inheritdoc />
        public IGenericHelp Help => DefaultParser.Help;
    }

    public class GitFlavor
    {
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

        public IEnumerable<IParser> GetParsers()
        {
            var parser = new GitParser(this);
            foreach (var @switch in Switches) parser.AddParameter(@switch);
            foreach (var positional in Positionals) parser.AddParameter(positional);

            var others = SubCommands.Values.SelectMany(x => x.GetParsers());
            return new[] {parser}.Concat(others);
        }

        public IParseResult Parse(string[] args)
        {
            var strat = new DefaultParseStrategy(FactoryMethods);
            return strat.Parse(GetParsers(), args);
        }

        public GitFlavor BaseFlavor { get; set; }
        public List<Func<object>> FactoryMethods { get; set; } = new List<Func<object>>();
        public List<Positional> Positionals { get; set; } = new List<Positional>();
        public Dictionary<string, GitFlavor> SubCommands { get; set; } = new Dictionary<string, GitFlavor>();
        public List<Switch> Switches { get; set; } = new List<Switch>();
    }
}