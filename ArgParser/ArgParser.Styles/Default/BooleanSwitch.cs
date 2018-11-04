using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class BooleanSwitch : Switch
    {
        public override bool CanConsume(object instance, IterationInfo info)
        {
            bool isLetter = Letter.HasValue && info.Current == $"-{Letter}";
            if (isLetter)
                return true;
            return info.Current == $"--{Word}";
        }

        public override IterationInfo Consume(object instance, IterationInfo info)
        {
            HasBeenConsumedInternal = true;
            return info.Consume(1);
        }

        protected internal bool HasBeenConsumedInternal { get; set; }

        public override bool HasBeenConsumed => HasBeenConsumedInternal;

        public BooleanSwitch(char? letter, string word) : base(letter, word)
        {
        }
    }
}