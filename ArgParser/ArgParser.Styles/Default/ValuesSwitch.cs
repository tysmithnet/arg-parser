using System;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class ValuesSwitch : Switch
    {
        public ValuesSwitch(Parser parent, char? letter, string word, Action<object, string[]> consumeCallback,
            int min = 1,
            int max = int.MaxValue) : base(parent, letter, word,
            (o, strings) => consumeCallback(o, strings.Skip(1).ToArray()))
        {
            MinRequired = min;
            MaxAllowed = max == int.MaxValue ? max : max + 1;
        }
    }

    public class ValuesSwitch<T> : Switch
    {
        public ValuesSwitch(Parser parent, char? letter, string word, Action<T, string[]> consumeCallback, int min = 1,
            int max = int.MaxValue) : base(parent, letter, word, consumeCallback.ToNonGenericAction())
        {
            MinRequired = min;
            MaxAllowed = max == int.MaxValue ? max : max + 1;
        }
    }
}