using System;
using System.Linq;

namespace ArgParser.Styles.Default
{
    public class ValuesSwitch : Switch
    {
        public ValuesSwitch(char? letter, string word, Action<object, string[]> consumeCallback, int min = 1,
            int max = int.MaxValue) : base(letter, word, (o, strings) => consumeCallback(o, strings.Skip(1).ToArray()))
        {
            MinRequired = min;
            MaxAllowed = max == int.MaxValue ? max : max + 1;
        }
    }
}