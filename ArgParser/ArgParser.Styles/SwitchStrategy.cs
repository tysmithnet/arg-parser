using System;
using ArgParser.Core;

namespace ArgParser.Styles
{
    public class SwitchStrategy : ISwitchStrategy
    {
        public char Token { get; set; } = '-';
        public Switch Switch { get; set; }

        public SwitchStrategy(Switch @switch)
        {
            Switch = @switch ?? throw new ArgumentNullException(nameof(@switch));
        }

        public bool IsWordMatch(IterationInfo info)
        {
            return Switch.Word.IsNotNullOrWhiteSpace() && info.Current == $"{Token}{Switch.Word}";
        }

        public bool IsLetterMatch(IterationInfo info)
        {
            return Switch.Letter.HasValue && info.Current == $"{Token}{Switch.Letter.Value}";
        }
    }
}