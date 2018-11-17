using System.Collections.Generic;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public class FullUsageVm
    {
        public IList<Parser> ParserChain { get; set; }
        public IList<Parameter> Parameters { get; set; }
    }
}