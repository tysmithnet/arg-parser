using System;
using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public abstract class Switch : Parameter
    {
        public char? Letter { get; protected internal set; }
        public string Word { get; protected internal set; }

        protected Switch(char? letter, string word)
        {
            if (letter == null && word == null)
            {
                throw new ArgumentException($"You must either provide a letter or a word to identify this switch");
            }
            Letter = letter;
            Word = word;
        }
    }
}