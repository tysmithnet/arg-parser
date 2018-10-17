using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public class DefaultSwitchStrategy<T> : ISwitchStrategy<T>
    {
        /// <inheritdoc />
        public DefaultSwitchStrategy()
        {
        }
        
        /// <inheritdoc />
        public IterationInfo ConsumeSwitch(IList<Switch<T>> switches, T instance, IterationInfo info, ISwitchStrategy<T> parentStrategy = null)
        {
            var first = switches.First(s => s.IsToken(info));
            var consumed = info.Rest.TakeWhile((e, i) => first.TakeWhile(info, e, i)).ToArray();
            first.Transformer(info, instance, consumed);
            info.Index += consumed.Length;
            return info;
        }

        /// <inheritdoc />
        public bool IsSwitch(IList<Switch<T>> switches, IterationInfo info, ISwitchStrategy<T> parentStrategy = null)
        {
            return switches.Any(s => s.IsToken(info)) || (parentStrategy?.IsSwitch(switches, info) ?? false);
        }

        public bool IsGroup(IList<Switch<T>> switches, IterationInfo info, ISwitchStrategy<T> parentStrategy = null)
        {
            if (!info.Cur.StartsWith("-"))
                return false;
            var sansHash = info.Cur.Substring(1);
            var letters = switches
                .Where(x => x.GroupLetter.HasValue)
                .Select(x => x.GroupLetter.Value)
                .Select(x => x.ToString())
                .Aggregate((l, r) =>
                {
                    if (l.Contains(r))
                        return r;
                    return l + r;
                })
                .ToCharArray();
            return sansHash.ToCharArray().All(c => letters.Contains(c)) || (parentStrategy?.IsGroup(switches, info) ?? false);
        }

        /// <inheritdoc />
        public IterationInfo ConsumeGroup(IList<Switch<T>> switches, T instance, IterationInfo info, ISwitchStrategy<T> parentStrategy = null)
        {
            if (IsGroup(switches, info))
            {
                var letters = switches
                    .Where(x => x.GroupLetter.HasValue)
                    .Select(x => x.GroupLetter.Value)
                    .Select(x => x.ToString())
                    .Aggregate((l, r) =>
                    {
                        if (l.Contains(r))
                            return r;
                        return l + r;
                    })
                    .ToCharArray();
                foreach (var letter in letters.Reverse().Skip(1))
                {
                    var s = switches.Where(x => x.GroupLetter.HasValue).First(x => x.GroupLetter.Value == letter);
                    s.Transformer(info, instance, new string[0]);
                }

                var last = switches.Where(x => x.GroupLetter.HasValue).First(x => x.GroupLetter.Value == letters.Last());
                var consumed = info.Rest.TakeWhile((e, i) => last.TakeWhile(info, e, i)).ToArray();
                last.Transformer(info, instance, consumed);
                info.Index += consumed.Length;
                return info;
            }
            else
            {
                if(parentStrategy == null)
                    throw new InvalidOperationException("Switch determined to have been found, but can't refind switch to process");
                return parentStrategy.ConsumeGroup(switches, instance, info);
            }
        }

        /// <inheritdoc />
        public void Reset()
        {
        }
    }
}