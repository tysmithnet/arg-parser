using System;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Flavors
{
    public class Positional : IParameter
    {
        /// <inheritdoc />
        public bool CanConsume(object instance, IIterationInfo info)
        {
            var ar = info.Rest.Select(x => TokenExtensions.ToGitToken(x)).TakeWhile(t => !t.IsAnyMatch).ToArray();
            return ar.Length >= Min && ar.Length < Max;
        }

        /// <inheritdoc />
        public IIterationInfo Consume(object instance, IIterationInfo info)
        {
            var tokens = info.Rest.Select(x => x.ToGitToken()).TakeWhile(t => !t.IsAnyMatch).Select(t => t.Raw)
                .ToArray();
            // todo: check count
            ConsumeCallback(instance, tokens);
            return info.Consume(1 + tokens.Length);
        }

        public Action<object, string[]> ConsumeCallback { get; set; }
        public int Max { get; set; } = int.MaxValue;
        public int Min { get; set; } = 1;
    }
}