using System;
using System.Collections.Generic;
using System.Text;
using ArgParser.Core;

namespace ArgParser.Styles.Help.Alba
{
    public class ContextDecorator
    {
        protected internal IContext Context { get; set; }

        public IEnumerable<Switch> OwnSwitches { get; set; }
        public IEnumerable<Switch> InheritedSwitches { get; set; }
        public IEnumerable<Positional> Positionals { get; set; }
        public IEnumerable<Positional> InheritedPositionals { get; set; }

    }
}
