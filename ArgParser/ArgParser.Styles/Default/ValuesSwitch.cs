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

    public class ValuesSwitch<T> : Switch
    {
        private static Action<object, string[]> Convert(Action<T, string[]> action)
        {
            return (instance, s) =>
            {
                if (instance is T casted)
                    action(casted, s);
                else
                    throw new ArgumentException(
                        $"Expected to find object of type={typeof(T).FullName}, but found type={instance.GetType().FullName}");
            };
        }

        public ValuesSwitch(char? letter, string word, Action<T, string[]> consumeCallback, int min = 1,
            int max = int.MaxValue) : base(letter, word, Convert(consumeCallback))
        {
            MinRequired = min;
            MaxAllowed = max == int.MaxValue ? max : max + 1;
        }
    }
}