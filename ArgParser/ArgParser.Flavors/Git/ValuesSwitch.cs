using System;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Flavors.Git
{
    public class ValuesSwitch : Switch
    {
        /// <inheritdoc />
        public override IIterationInfo Consume(object instance, IIterationInfo info)
        {
            var tokens = info.Rest.Select(x => TokenExtensions.ToGitToken(x)).TakeWhile(t => !t.IsAnyMatch).Select(t => t.Raw)
                .ToArray();
            // todo: check count
            ConsumeCallback(instance, tokens);
            HasBeenConsumed = true;
            return info.Consume(1 + tokens.Length);
        }

        /// <inheritdoc />
        public override bool HasBeenConsumed { get; set; }

        public Action<object, string[]> ConsumeCallback { get; set; }
    }
}