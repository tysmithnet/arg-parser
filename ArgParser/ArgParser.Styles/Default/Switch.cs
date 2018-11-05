using System;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public abstract class Switch : Parameter
    {
        public char? Letter { get; protected internal set; }
        public string Word { get; protected internal set; }

        protected Switch(char? letter, string word, Action<object, string[]> consumeCallback) : base(consumeCallback)
        {
            if (letter == null && word == null)
            {
                throw new ArgumentException($"You must either provide a letter or a word to identify this switch");
            }
            Letter = letter;
            Word = word;
        }

        public override string ToString()
        {
            if (Letter != null && Word != null)
                return $"-{Letter}, --{Word}";
            return Letter.HasValue ? $"-{Letter}" : $"--{Word}";
        }

        public virtual bool IsLetterMatch(IterationInfo info)
        {
            return Letter.HasValue && info.Current == $"-{Letter}";
        }

        public virtual bool IsWordMatch(IterationInfo info)
        {
            return Word != null && info.Current == $"--{Word}";
        }

        public override ConsumptionResult CanConsume(object instance, IterationInfo info)
        {
            if (IsLetterMatch(info) || IsWordMatch(info))
                return new ConsumptionResult(info, MaxAllowed);
            return new ConsumptionResult(info, MaxAllowed);
        }
    }
}