using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public class IterationInfo : IIterationInfo
    {
        /// <inheritdoc />
        public IToken Current => Tokens?[Index];

        /// <inheritdoc />
        public IToken Next => Tokens.Count < Index + 1 ? Tokens[Index + 1] : null;

        /// <inheritdoc />
        public IReadOnlyList<IToken> Tokens { get; internal set; }

        /// <inheritdoc />
        public int Index { get; set; }

        public IIterationInfo SetTokens(IList<IToken> tokens)
        {
            return Clone(info => info.Tokens = tokens?.ToList());
        }

        private IterationInfo Clone(Action<IterationInfo> transformer)
        {
            var newGuy = new IterationInfo()
            {
                Args = Args?.ToArray(),
                Tokens = Tokens?.ToList(),
                Index = Index,
            };
            transformer?.Invoke(newGuy);
            return newGuy;
        }

        public IIterationInfo SetIndex(int i)
        {
            return Clone(info => info.Index = i);
        }

        /// <inheritdoc />
        public string[] Args { get; set; }

        /// <inheritdoc />
        public IIterationInfo Consume(int numTokens)
        {
            return SetIndex(Index + numTokens);
        }
    }
}