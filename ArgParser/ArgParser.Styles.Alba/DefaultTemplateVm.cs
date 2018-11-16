using System.Collections.Generic;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public class DefaultTemplateVm
    {
        public string ParserId { get; set; }
        public Parser Parser { get; set; }
        public IContext Context { get; set; }
    }
}