using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public class DefaultSwitchStrategy<T> : ISwitchStrategy<T>
    {
        protected internal IPositionalStrategy<T> PositionalStrategy { get; set; }
        protected internal IList<Positional<T>> Positionals { get; set; }
        protected internal IList<Switch<T>> Switches { get; set; }

        /// <inheritdoc />
        public IterationInfo ConsumeGroup(IList<Switch<T>> switches, T instance, IterationInfo info)
        {
            var switchList = info.Cur.Substring(1).ToCharArray();
            var notLast = switchList.Reverse().Skip(1).Reverse().ToArray();
            foreach (var n in notLast)
            {
                var currentSwitch = switches.Where(x => x.GroupLetter.HasValue).First(x => x.GroupLetter.Value == n);
                currentSwitch.Transformer?.Invoke(info, instance, new string[0]);
            }

            var lastLetter = switchList.Last();
            var lastSwitch = switches.Where(x => x.GroupLetter.HasValue).First(x => x.GroupLetter.Value == lastLetter);
            var consumed = info.Rest.TakeWhile((e, i) =>
            {
                var clone = info.Clone();
                clone.Index += i + 1;
                var takeWhile = lastSwitch.TakeWhile(info, e, i);
                var isSwitch = clone.Index < info.AllArgs.Length && IsSwitch(switches, clone);
                var isGroup = clone.Index < info.AllArgs.Length && IsGroup(switches, clone);
                return takeWhile && !isSwitch && !isGroup;
            }).ToArray();
            lastSwitch.Transformer?.Invoke(info, instance, consumed);
            info.Index += consumed.Length + 1;
            return info;
        }

        /// <inheritdoc />
        public IterationInfo ConsumeSwitch(IList<Switch<T>> switches, T instance, IterationInfo info)
        {
            var first = switches.First(s => s.IsToken(info));
            var consumed = info.Rest.TakeWhile((e, i) =>
            {
                var clone = info.Clone();
                clone.Index += i + 1;
                var takeWhile = first.TakeWhile(info, e, i);
                var isSwitch = clone.Index < info.AllArgs.Length && IsSwitch(switches, clone);
                var isGroup = clone.Index < info.AllArgs.Length && IsGroup(switches, clone);
                return takeWhile && !isSwitch && !isGroup;
            }).ToArray();
            first.Transformer(info, instance, consumed);
            info.Index += consumed.Length + 1;
            return info;
        }

        public bool IsGroup(IList<Switch<T>> switches, IterationInfo info)
        {
            if (!info.Cur.StartsWith("-"))
                return false;
            var sansHash = info.Cur.Substring(1).ToCharArray();
            var letters = switches
                .Where(x => x.GroupLetter.HasValue)
                .Select(x => x.GroupLetter.Value)
                .Select(x => x.ToString())
                .ToArray();
                
            return sansHash.All(x => letters.Contains($"{x}"));
        }

        /// <inheritdoc />
        public bool IsSwitch(IList<Switch<T>> switches, IterationInfo info)
        {
            return switches.Any(s => s.IsToken?.Invoke(info) ?? false);
        }

        /// <inheritdoc />
        public void Reset()
        {
        }
    }
}