using System;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class SingleValueSwitch : Switch
    {
        public SingleValueSwitch(char? letter, string word, Action<object, string> consumeCallback) : base(letter, word, (o, strings) => consumeCallback(o, strings.Skip(1).First()))
        {
            MinRequired = 2;
            MaxAllowed = 2;
        }
    }
}