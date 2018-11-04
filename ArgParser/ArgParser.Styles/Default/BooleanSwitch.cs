using System;
using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class BooleanSwitch : Switch
    {
        public override bool CanConsume(object instance, IterationInfo info)
        {
            bool isLetter = Letter.HasValue && info.Current == $"-{Letter}";
            if (isLetter)
                return true;
            return info.Current == $"--{Word}";
        }

        public override IterationInfo Consume(object instance, IterationInfo info)
        {

            return base.Consume(instance, info).Consume(1);
        }

        public BooleanSwitch(char? letter, string word, Action<object> consumeCallback) : base(letter, word)
        {
            ConsumeCallback = consumeCallback.ThrowIfArgumentNull(nameof(consumeCallback));
        }

        public Action<object> ConsumeCallback { get; set; }
    }

    public class SingleValueSwitch : Switch
    {
        public Action<object, string> ConsumeCallback { get; protected internal set; }
        public SingleValueSwitch(char? letter, string word, Action<object, string> consumeCallback) : base(letter, word)
        {
            ConsumeCallback = consumeCallback.ThrowIfArgumentNull(nameof(consumeCallback));
        }
        
        public override IterationInfo Consume(object instance, IterationInfo info)
        {
            if (!info.HasNext())
                throw new MissingValueException($"Expected to find a value for {this}");
            ConsumeCallback(instance, info.Next());
            return base.Consume(instance, info).Consume(2);
        }
    }

    public class ValuesSwitch : Switch
    {

        public Action<object, string[]> ConsumeCallback { get; protected internal set; }

        public ValuesSwitch(char? letter, string word, Action<object, string[]> consumeCallback) : base(letter, word)
        {
            ConsumeCallback = consumeCallback.ThrowIfArgumentNull(nameof(consumeCallback));
        }

        public override IterationInfo Consume(object instance, IterationInfo info)
        {
            
            return base.Consume(instance, info).
        }
    }

    public class MissingValueException : ParseException
    {
        public MissingValueException(string message) : base(message)
        {
        }
    }
}