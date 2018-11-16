using System.Collections.Generic;
using ArgParser.Core;
using ArgParser.Core.Help;

namespace ArgParser.Styles.Alba
{
    public class DefaultTemplateVm
    {
        public string ParserId { get; set; }
        public Parser Parser { get; set; }
        public IContext Context { get; set; }
        public IList<Example> Examples { get; set; }
        public string SubTitle => $"{Parser.Help.Name} - {Parser.Help.ShortDescription}";
    }
}