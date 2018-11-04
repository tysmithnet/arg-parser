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
}