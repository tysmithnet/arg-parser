using System;
using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class BooleanSwitch : Switch
    {
        public override IterationInfo Consume(object instance, ConsumptionRequest request)
        {
            ConsumeCallback(instance);
            return base.Consume(instance, request);
        }

        public BooleanSwitch(char? letter, string word, Action<object> consumeCallback) : base(letter, word)
        {
            ConsumeCallback = consumeCallback.ThrowIfArgumentNull(nameof(consumeCallback));
            MinRequired = 1;
            MaxRequired = 1;
        }

        public Action<object> ConsumeCallback { get; set; }
    }

    public class SingleValueSwitch : Switch
    {
        public Action<object, string> ConsumeCallback { get; protected internal set; }
        public SingleValueSwitch(char? letter, string word, Action<object, string> consumeCallback) : base(letter, word)
        {
            ConsumeCallback = consumeCallback.ThrowIfArgumentNull(nameof(consumeCallback));
            MinRequired = 2;
            MaxRequired = 2;
        }

        public override IterationInfo Consume(object instance, ConsumptionRequest request)
        {
            ConsumeCallback(instance, request.Next());
            return base.Consume(instance, request);
        }
    }

    public class ValuesSwitch : Switch
    {

        public ValuesSwitch(char? letter, string word, Action<object, string[]> consumeCallback) : base(letter, word)
        {
        }
    }

    public class MissingValueException : ParseException
    {
        public MissingValueException(string message) : base(message)
        {
        }
    }
}