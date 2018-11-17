using System;
using System.Collections.Generic;
using ArgParser.Core;
using ArgParser.Core.Help;

namespace ArgParser.Styles.Alba
{
    public class DefaultTemplateVm : IViewModel
    {
        public string ParserId { get; set; }
        public Parser Parser { get; set; }
        public AlbaContext Context { get; set; }
        public IList<Example> Examples { get; set; }
        public string SubTitle => $"{Parser.Help.Name} - {Parser.Help.ShortDescription}";
        public Theme Theme { get; set; }

        public DefaultTemplateVm(Parser parser, AlbaContext context, Theme theme)
        {
            Parser = parser ?? throw new ArgumentNullException(nameof(parser));
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Theme = theme ?? throw new ArgumentNullException(nameof(theme));
            ParserId = Parser.Id;
        }
    }
}