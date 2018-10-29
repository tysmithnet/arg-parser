using System;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Flavors
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
            return info.Consume(1 + tokens.Length);
        }

        public Action<object, string[]> ConsumeCallback { get; set; }
    }
}