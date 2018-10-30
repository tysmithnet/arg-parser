﻿using System;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Flavors.Git
{
    public class Positional : GitParameter
    {
        /// <inheritdoc />
        public override bool CanConsume(object instance, IIterationInfo info)
        {
            if (IsConsumed)
                return false;
            var ar = info.FromNowOn().Select(x => x.ToGitToken()).TakeWhile(t => !t.IsAnyMatch).ToArray();
            return ar.Length >= Min;
        }

        /// <inheritdoc />
        public override IIterationInfo Consume(object instance, IIterationInfo info)
        {
            var tokens = info.FromNowOn().Select(x => x.ToGitToken()).TakeWhile(t => !t.IsAnyMatch).Take(Max).Select(t => t.Raw)
                .ToArray();
            // todo: check count
            ConsumeCallback(instance, tokens);
            IsConsumed = true;
            return info.Consume(tokens.Length);
        }

        /// <inheritdoc />
        public override bool HasBeenConsumed { get; set; }
        
        public bool IsConsumed { get; set; }
        public Action<object, string[]> ConsumeCallback { get; set; }
        public int Max { get; set; } = int.MaxValue;
        public int Min { get; set; } = 1;
    }
}