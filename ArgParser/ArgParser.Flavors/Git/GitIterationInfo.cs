using System;
using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;
using ArgParser.Core.Validation;

namespace ArgParser.Flavors.Git
{
    public class GitIterationInfo : IIterationInfo
    {
        public GitIterationInfo(string[] args, IEnumerable<GitToken> tokens)
        {
            Args = args ?? new string[0];
            GitTokens = tokens.PreventNull().ToList();
        }

        public IIterationInfo Consume(int numTokens)
        {
            var clone = Clone();
            clone.Index += numTokens;
            return clone;
        }

        protected internal GitIterationInfo Clone()
        {
            var clone = new GitIterationInfo(Args, GitTokens)
            {
                ParseErrors = ParseErrors.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToList())
            };
            
            return clone;
        }

        private Dictionary<int, List<ParseException>> ParseErrors { get; set; } = new Dictionary<int, List<ParseException>>();
        
        public void AddErrors(params ParseException[] parseExceptions)
        {
            if(!ParseErrors.ContainsKey(Index))
                ParseErrors.Add(Index, new List<ParseException>());
            ParseErrors[Index].AddRange(parseExceptions);
        }

        public string[] Args { get; }

        public IToken Current => Tokens[Index];

        public IToken First => Tokens[0];

        public int Index { get; protected internal set; }

        public bool IsComplete => Index >= Tokens.Count;

        public bool IsFirst => Index == 0;

        public bool IsInternal => Index > 0 && Index < Tokens.Count;

        public bool IsLast => Index == Tokens.Count - 1;

        public IToken Last => Tokens.Last();

        public IToken Next => Tokens[Index + 1];

        public IEnumerable<IToken> Rest => Tokens.Skip(Index + 1);
        protected internal IList<GitToken> GitTokens { get; set; }

        public IReadOnlyList<IToken> Tokens => GitTokens.ToList();
    }
}