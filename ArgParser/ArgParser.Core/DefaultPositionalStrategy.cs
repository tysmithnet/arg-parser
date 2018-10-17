using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public class DefaultPositionalStrategy<T> : IPositionalStrategy<T>
    {
        protected internal IList<Positional<T>> Positionals { get; set; }
        protected internal ISet<object> Seen = new HashSet<object>();

        /// <inheritdoc />
        public IterationInfo Consume(T instance, IterationInfo info)
        {
            var first = Positionals.First(p => !Seen.Contains(p));
            var consumed = info.Rest.TakeWhile((e, i) => first.TakeWhile(info, e, i)).ToArray();
            first.Transformer(info, instance, consumed);
            info.Index += consumed.Length;
            return info;
        }

        /// <inheritdoc />
        public bool IsPositional(IterationInfo info)
        {
            return Positionals.Any(p => !Seen.Contains(p));
        }

        /// <inheritdoc />
        public void Reset()
        {
            Seen.Clear();
        }
    }
}