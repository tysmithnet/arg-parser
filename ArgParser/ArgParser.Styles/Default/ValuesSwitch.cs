using System;

namespace ArgParser.Styles.Default
{
    public class ValuesSwitch : Switch
    {
        public ValuesSwitch(char? letter, string word, Action<object, string[]> consumeCallback, int min = 1, int max = int.MaxValue) : base(letter, word, consumeCallback)
        {
            MinRequired = min;
            MaxAllowed = max;
        }
    }
}