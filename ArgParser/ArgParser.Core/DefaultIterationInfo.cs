using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public class DefaultIterationInfo : IIterationInfo
    {
        /// <inheritdoc />
        public bool IsComplete => Index >= Tokens?.Count;

        /// <inheritdoc />
        public IToken Current => !IsComplete ? Tokens?[Index] : null;

        /// <inheritdoc />
        public IToken Next => Rest?.FirstOrDefault();

        /// <inheritdoc />
        public IReadOnlyList<IToken> Tokens { get; internal set; }

        /// <inheritdoc />
        public IEnumerable<IToken> Rest => Tokens?.Skip(Index + 1);

        /// <inheritdoc />
        public int Index { get; set; }

        public IIterationInfo SetTokens(IList<IToken> tokens)
        {
            return Clone(info => info.Tokens = tokens?.ToList());
        }

        private DefaultIterationInfo Clone(Action<DefaultIterationInfo> transformer)
        {
            var newGuy = new DefaultIterationInfo()
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