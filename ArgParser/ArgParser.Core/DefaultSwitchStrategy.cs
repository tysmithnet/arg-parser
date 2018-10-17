using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public class DefaultSwitchStrategy<T> : ISwitchStrategy<T>
    {
        /// <inheritdoc />
        protected internal IList<TokenSwitch<T>> Switches { get; set; }

        /// <inheritdoc />
        public IterationInfo ConsumeSwitch(T instance, IterationInfo info)
        {
            var first = Switches.First(s => s.IsToken(info));
            var consumed = info.Rest.TakeWhile((e, i) => first.TakeWhile(info, e, i)).ToArray();
            first.Transformer(info, instance, consumed);
            info.Index += consumed.Length;
            return info;
        }

        /// <inheritdoc />
        public bool IsSwitch(IterationInfo info)
        {
            return Switches.Any(s => s.IsToken(info));
        }

        public bool IsGroup(IterationInfo info)
        {
            if (!info.Cur.StartsWith("-"))
                return false;
            var sansHash = info.Cur.Substring(1);
            var letters = Switches
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
            return sansHash.ToCharArray().All(c => letters.Contains(c));
        }

        /// <inheritdoc />
        public IterationInfo ConsumeGroup(T instance, IterationInfo info)
        {
            var letters = Switches
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
                var s = Switches.Where(x => x.GroupLetter.HasValue).First(x => x.GroupLetter.Value == letter);
                s.Transformer(info, instance, new string[0]);
            }

            var last = Switches.Where(x => x.GroupLetter.HasValue).First(x => x.GroupLetter.Value == letters.Last());
            var consumed = info.Rest.TakeWhile((e, i) => last.TakeWhile(info, e, i)).ToArray();
            last.Transformer(info, instance, consumed);
            info.Index += consumed.Length;
            return info;
        }

        /// <inheritdoc />
        public void Reset()
        {
        }
    }
}