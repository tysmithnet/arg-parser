using System;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Flavors.Git
{
    public class Positional : GitParameter
    {
        public Positional(Action<object, string[]> consumeCallback)
        {
            ConsumeCallback = consumeCallback ?? throw new ArgumentNullException(nameof(consumeCallback));
        }

        internal Positional()
        {
        }

        public override bool CanConsume(object instance, IIterationInfo info)
        {
            if (HasBeenConsumed)
                return false;
            var ar = info.FromNowOn().Select(x => x.ToGitToken()).TakeWhile(t => !t.IsAnyMatch).ToArray();
            return ar.Length >= Min;
        }

        public override IIterationInfo Consume(object instance, IIterationInfo info)
        {
            var tokens = info.FromNowOn().Select(x => x.ToGitToken()).TakeWhile(t => !t.IsAnyMatch).Take(Max)
                .Select(t => t.Raw)
                .ToArray();
            // todo: check count
            ConsumeCallback(instance, tokens);
            HasBeenConsumed = true;
            return info.Consume(tokens.Length);
        }

        public Action<object, string[]> ConsumeCallback { get; set; }

        public override bool HasBeenConsumed { get; set; }

        public int Max { get; set; } = int.MaxValue;
        public int Min { get; set; } = 1;
    }
}