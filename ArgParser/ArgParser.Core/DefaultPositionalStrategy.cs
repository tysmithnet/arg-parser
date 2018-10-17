using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public class DefaultPositionalStrategy<T> : IPositionalStrategy<T>
    {
        protected internal ISet<object> Seen = new HashSet<object>();
        
        public IList<CommandLineElement<T>> OrderOfAdditions { get; set; }
        
        /// <inheritdoc />
        public void Reset()
        {
            Seen.Clear();
        }

        /// <inheritdoc />
        public IterationInfo Consume(IList<Positional<T>> positionals, T instance, IterationInfo info, IPositionalStrategy<T> parent = null)
        {
            var first = positionals.FirstOrDefault(p => !Seen.Contains(p));
            if (first != null)
            {
                Seen.Add(first);
                var consumed = info.CurOn.TakeWhile((e, i) => first.TakeWhile(info, e, i)).ToArray();
                first.Transformer(info, instance, consumed);
                info.Index += consumed.Length;
                return info;
            }
            else
            {
                if (parent == null)
                {
                    throw new InvalidOperationException($"Positional determined to have been found, but unable to find it again.");
                }

                return parent.Consume(positionals, instance, info);
            }
        }

        /// <inheritdoc />
        public bool IsPositional(IList<Positional<T>> positionals, IterationInfo info, IPositionalStrategy<T> parent = null)
        {
            return positionals.Any(p => !Seen.Contains(p)) || (parent?.IsPositional(positionals, info) ?? false);
        }
    }
}