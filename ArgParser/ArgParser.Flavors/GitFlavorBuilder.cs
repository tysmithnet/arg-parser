using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ArgParser.Core;

namespace ArgParser.Flavors
{
    

    public class GitParseStrategy : IParseStrategy
    {
        public class GitLexer : ILexer
        {
            /// <inheritdoc />
            public IEnumerable<IToken> Lex(string[] args)
            {
                return args.SelectMany(s =>
                {
                    var match = Regex.Match(s, "^-(?<letters>[a-zA-Z0-9]{2,}$)");
                    if (!match.Success) return new[] { new DefaultToken(s) };
                    var letters = match.Groups["letters"].Value.ToCharArray();
                    return letters.Select(c => new DefaultToken($"-{c}"));
                });
            }
        }

        /// <inheritdoc />
        public IParseResult Parse(IEnumerable<IParser> parsers, string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
