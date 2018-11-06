using System;
using System.Linq;

namespace ArgParser.Styles.Default
{
    public class SingleValueSwitch : Switch
    {
        public SingleValueSwitch(char? letter, string word, Action<object, string> consumeCallback) : base(letter, word,
            (o, strings) => consumeCallback(o, strings.Skip(1).First()))
        {
            MinRequired = 2;
            MaxAllowed = 2;
        }
    }

    public class SingleValueSwitch<T> : SingleValueSwitch
    {
        private static Action<object, string> Convert(Action<T, string> action)
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

        public SingleValueSwitch(char? letter, string word, Action<T, string> consumeCallback) : base(letter, word, Convert(consumeCallback))
        {
        }
    }
}