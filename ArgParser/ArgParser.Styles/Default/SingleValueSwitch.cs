using System;
using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class SingleValueSwitch : Switch
    {
        public SingleValueSwitch(char? letter, string word, Action<object, string> consumeCallback) : base(letter, word, consumeCallback.ToMultiValueAction())
        {
            MinRequired = 2;
            MaxAllowed = 2;
        }
    }
}