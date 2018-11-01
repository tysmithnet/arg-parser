using System;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Flavors.Git
{
    public class ValuesSwitch : Switch
    {
        public override IIterationInfo Consume(object instance, IIterationInfo info)
        {
            var tokens = info.Rest.Select(x => x.ToGitToken()).TakeWhile(t => !t.IsAnyMatch).Select(t => t.Raw)
                .ToArray();
            if (tokens.Length < Min)
                throw new IndexOutOfRangeException($"Expected to find at least {Min} tokens, but found {tokens.Length} tokens. Check to see that you are passing enough values to -{Letter}/--{Word}");
            ConsumeCallback(instance, tokens);
            HasBeenConsumed = true;
            return info.Consume(1 + tokens.Length);
        }

        public Action<object, string[]> ConsumeCallback { get; set; }
        public int Min { get; set; } = 0; // todo: positive values
        public int Max { get; set; } = int.MaxValue;
        public override bool HasBeenConsumed { get; set; }
    }
}