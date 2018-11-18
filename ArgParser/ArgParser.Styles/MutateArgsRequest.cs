using System.Collections.Generic;
using ArgParser.Core;

namespace ArgParser.Styles
{
    public class MutateArgsRequest
    {
        public string[] Args { get; set; }
        public IEnumerable<Parser> Chain { get; set; }
        public IContext Context { get; set; }
    }
}