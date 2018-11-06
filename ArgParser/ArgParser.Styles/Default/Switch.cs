﻿using System;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public abstract class Switch : Parameter
    {
        protected Switch(char? letter, string word, Action<object, string[]> consumeCallback) : base(consumeCallback)
        {
            if (letter == null && word == null)
                throw new ArgumentException($"You must either provide a letter or a word to identify this switch");
            Letter = letter;
            Word = word;
        }

        public override ConsumptionResult CanConsume(object instance, IterationInfo info)
        {
            if (!IsLetterMatch(info) && !IsWordMatch(info)) return new ConsumptionResult(info, 0);
            var canBeTaken = info.FromNowOn().ToList();
            if (canBeTaken.Count < MinRequired)
                return new ConsumptionResult(info, 0);
            var actuallyTaken = canBeTaken.Take(MaxAllowed).ToList();
            return new ConsumptionResult(info, actuallyTaken.Count);
        }

        public virtual bool IsLetterMatch(IterationInfo info) => Letter.HasValue && info.Current == $"-{Letter}";

        public virtual bool IsWordMatch(IterationInfo info) => Word != null && info.Current == $"--{Word}";

        public override string ToString()
        {
            if (Letter != null && Word != null)
                return $"-{Letter}, --{Word}";
            return Letter.HasValue ? $"-{Letter}" : $"--{Word}";
        }

        public char? Letter { get; protected internal set; }
        public string Word { get; protected internal set; }
    }

    public abstract class Switch<T> : Switch
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

        protected Switch(char? letter, string word, Action<T, string[]> consumeCallback) : base(letter, word, Convert(consumeCallback))
        {
        }
    }
}