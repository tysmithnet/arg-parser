using System;
using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class BooleanSwitch : Switch
    {
        public override IterationInfo CanConsume(object instance, IterationInfo info)
        {
            return IsLetterMatch(info) || IsWordMatch(info) ? info.Consume(1) : info;
        }

        public override IterationInfo Consume(object instance, ConsumptionRequest request)
        {
            return request.Info.Consume(1);
        }

        public BooleanSwitch(char? letter, string word, Action<object> consumeCallback) : base(letter, word)
        {
            ConsumeCallback = consumeCallback.ThrowIfArgumentNull(nameof(consumeCallback));
        }

        public Action<object> ConsumeCallback { get; set; }
    }

    public class MissingValueException : ParseException
    {
        public MissingValueException(string message) : base(message)
        {
        }
    }
}