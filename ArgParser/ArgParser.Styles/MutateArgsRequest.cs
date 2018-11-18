using System.Collections.Generic;
using ArgParser.Core;

namespace ArgParser.Styles
{
    public class MutateArgsRequest
    {
        public IContext Context { get; set; }
        public string[] Args { get; set; }
        public IEnumerable<Parser> Chain { get; set; }
    }
}