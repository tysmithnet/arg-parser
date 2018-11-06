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
        public BooleanSwitch(char? letter, string word, Action<T> consumeCallback) : base(letter, word, consumeCallback.ToNonGenericAction())
        {
        }
    }
}