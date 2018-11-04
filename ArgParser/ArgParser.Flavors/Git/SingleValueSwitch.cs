using System;
using System.Collections.Generic;
using ArgParser.Core;
using ArgParser.Core.Validation;

namespace ArgParser.Flavors.Git
{
    public class SingleValueSwitch<T> : SingleValueSwitch
    {
        public SingleValueSwitch(char? letter, string word, Action<T, string> consumeCallback) : base(letter, word,
            consumeCallback.ToBaseCallback())
        {
        }
    }

    public class SingleValueSwitch : Switch
    {
        public SingleValueSwitch(char? letter, string word, Action<object, string> consumeCallback)
        {
            Letter = letter;
            Word = word.ThrowIfArgumentNull(nameof(word));
            ConsumeCallback = consumeCallback.ThrowIfArgumentNull(nameof(consumeCallback));
        }

        internal SingleValueSwitch()
        {
        }

        public override IIterationInfo Consume(object instance, IIterationInfo info)
        {
            var casted = info.ToGitIterationInfo();
            if (casted.Next == null)
                throw new IndexOutOfRangeException($"Single value switch trying to consume, but cannot get next token");
            ConsumeCallback(instance, casted.Next.Raw);
            HasBeenConsumed = true;
            return casted.Consume(2);
        }

        public Action<object, string> ConsumeCallback { get; set; }
        public override bool HasBeenConsumed { get; set; }
        public Func<string, IEnumerable<ParseException>> ValidityCallback { get; set; }
    }
}