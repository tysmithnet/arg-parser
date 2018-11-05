using System;
using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class BooleanSwitch : Switch
    {
        public BooleanSwitch(char? letter, string word, Action<object> consumeCallback) : base(letter, word, (o, strings) => consumeCallback(o))
        {
            MinRequired = 1;
            MaxAllowed = 1;
        }
    }
}