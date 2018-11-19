using System;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public class ParserViewModel
    {
        public Parser Parser { get; set; }
        public Theme Theme { get; set; }
        public string Alias { get; set; }
        public string DisplayString => Alias.IsNotNullOrWhiteSpace() ? Alias : Parser.Id;

        public ParserViewModel(Parser parser, Theme theme)
        {
            Parser = parser ?? throw new ArgumentNullException(nameof(parser));
            Theme = theme ?? throw new ArgumentNullException(nameof(theme));
        }
    }
}