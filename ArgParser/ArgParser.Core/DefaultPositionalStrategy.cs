﻿using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public class DefaultPositionalStrategy<T> : IPositionalStrategy<T>
    {
        protected internal ISet<object> Seen = new HashSet<object>();

        /// <inheritdoc />
        public IterationInfo Consume(IList<Positional<T>> positionals, T instance, IterationInfo info)
        {
            var first = positionals.First(p => !Seen.Contains(p));

            Seen.Add(first);
            var consumed = info.CurOn.TakeWhile((e, i) => first.TakeWhile(info, e, i)).ToArray();
            first.Transformer?.Invoke(info, instance, consumed);
            info.Index += consumed.Length;
            return info;
        }

        /// <inheritdoc />
        public bool IsPositional(IList<Positional<T>> positionals, IterationInfo info)
        {
            return positionals.Any(p => !Seen.Contains(p));
        }

        /// <inheritdoc />
        public void Reset()
        {
            Seen.Clear();
        }

        public IList<CommandLineElement<T>> OrderOfAdditions { get; set; }
    }
}