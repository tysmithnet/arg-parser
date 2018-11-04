using System;
using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class BooleanSwitch : Switch
    {
        public BooleanSwitch(char? letter, string word, Action<object> consumeCallback) : base(letter, word, consumeCallback.ToMultiValueAction())
        {
            MinRequired = 1;
            MaxAllowed = 1;
        }
    }

    public class SingleValueSwitch : Switch
    {
        public SingleValueSwitch(char? letter, string word, Action<object, string> consumeCallback) : base(letter, word, consumeCallback.ToMultiValueAction())
        {
            MinRequired = 2;
            MaxAllowed = 2;
        }
    }

    public class ValuesSwitch : Switch
    {
        public ValuesSwitch(char? letter, string word, Action<object, string[]> consumeCallback, int min = 1, int max = int.MaxValue) : base(letter, word, consumeCallback)
        {
            MinRequired = min;
            MaxAllowed = max;
        }
    }

    public class MissingValueException : ParseException
    {
        public MissingValueException(string message) : base(message)
        {
        }
    }
}