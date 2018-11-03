using System;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Flavors.Git
{
    public class ValuesSwitch<T> : ValuesSwitch
    {
        public ValuesSwitch(char? letter, string word, Action<T, string[]> consumeCallback) : base(letter, word,
            consumeCallback.ToBaseCallback())
        {
        }
    }

    public class ValuesSwitch : Switch
    {
        public ValuesSwitch(char? letter, string word, Action<object, string[]> consumeCallback)
        {
            Letter = letter;
            Word = word.ThrowIfArgumentNull(nameof(word));
            ConsumeCallback = consumeCallback.ThrowIfArgumentNull(nameof(consumeCallback));
        }

        internal ValuesSwitch()
        {
        }

        public override IIterationInfo Consume(object instance, IIterationInfo info)
        {
            var tokens = info.Rest.Select(x => x.ToGitToken()).TakeWhile(t => !t.IsAnyMatch).Select(t => t.Raw)
                .ToArray();
            if (tokens.Length < Min)
                throw new IndexOutOfRangeException(
                    $"Expected to find at least {Min} tokens, but found {tokens.Length} tokens. Check to see that you are passing enough values to -{Letter}/--{Word}");
            ConsumeCallback(instance, tokens);
            HasBeenConsumed = true;
            return info.Consume(1 + tokens.Length);
        }

        public Action<object, string[]> ConsumeCallback { get; set; }
        public override bool HasBeenConsumed { get; set; }
        public int Max { get; set; } = int.MaxValue;
        public int Min { get; set; } = 0; // todo: positive values
    }
}