using System;
using ArgParser.Core;

namespace ArgParser.Flavors.Git
{
    public class BooleanSwitch : Switch
    {
        public BooleanSwitch(char letter, string word, Action<object> consumeCallback)
        {
            Letter = letter;
            Word = word.ThrowIfArgumentNull(nameof(word));
            ConsumeCallback = consumeCallback.ThrowIfArgumentNull(nameof(consumeCallback));
        }

        internal BooleanSwitch()
        {
        }

        public override IIterationInfo Consume(object instance, IIterationInfo info)
        {
            ConsumeCallback(instance);
            HasBeenConsumed = true;
            return info.Consume(1);
        }

        public Action<object> ConsumeCallback { get; set; }

        public override bool HasBeenConsumed { get; set; }
    }
}