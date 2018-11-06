using System;
using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class BooleanSwitch : Switch
    {
        public BooleanSwitch(char? letter, string word, Action<object> consumeCallback) : base(letter, word,
            (o, strings) => consumeCallback(o))
        {
            MinRequired = 1;
            MaxAllowed = 1;
        }
    }

    public class BooleanSwitch<T> : BooleanSwitch
    {
        private static Action<object> Convert(Action<T> toConvert)
        {
            // todo: does this belong here?
            toConvert.ThrowIfArgumentNull(nameof(toConvert));
            return instance =>
            {
                instance.ThrowIfArgumentNull(nameof(instance));
                if (instance is T casted)
                    toConvert(casted);
                else
                    throw new ArgumentException($"Expected to find object of type={typeof(T).FullName}, but found type={instance.GetType().FullName}");
            };
        }

        public BooleanSwitch(char? letter, string word, Action<T> consumeCallback) : base(letter, word, Convert(consumeCallback))
        {
        }
    }
}