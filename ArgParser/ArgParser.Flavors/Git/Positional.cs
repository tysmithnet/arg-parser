using System;
using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;
using ArgParser.Core.Validation;

namespace ArgParser.Flavors.Git
{
    public class Positional<T> : Positional
    {
        public Positional(Action<T, string[]> consumeCallback) : base(consumeCallback.ToBaseCallback())
        {
        }
    }

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
            if (tokens.Length < Min)
                throw new IndexOutOfRangeException($"Positional expected at least {Min} but found {tokens.Length}");
            ConsumeCallback(instance, tokens);
            HasBeenConsumed = true;
            return info.Consume(tokens.Length);
        }

        public Action<object, string[]> ConsumeCallback { get; set; }
        public Func<string[], IEnumerable<ParseException>> ValidityCallback { get; set; }
        public override bool HasBeenConsumed { get; set; }

        public int Max { get; set; } = int.MaxValue;
        public int Min { get; set; } = 1;
    }
}