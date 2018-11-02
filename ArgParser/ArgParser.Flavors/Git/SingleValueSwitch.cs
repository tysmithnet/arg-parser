using System;
using ArgParser.Core;

namespace ArgParser.Flavors.Git
{
    public class SingleValueSwitch : Switch
    {
        public SingleValueSwitch(char letter, string word, Action<object, string> consumeCallback)
        {
            Letter = letter;
            Word = word;
            ConsumeCallback = consumeCallback;
        }

        internal SingleValueSwitch()
        {
        }

        public override IIterationInfo Consume(object instance, IIterationInfo info)
        {
            if (info.Next == null)
                throw new IndexOutOfRangeException($"Single value switch trying to consume, but cannot get next token");
            ConsumeCallback(instance, info.Next.Raw); // todo: check
            HasBeenConsumed = true;
            return info.Consume(2);
        }

        public Action<object, string> ConsumeCallback { get; set; }

        public override bool HasBeenConsumed { get; set; }
    }
}