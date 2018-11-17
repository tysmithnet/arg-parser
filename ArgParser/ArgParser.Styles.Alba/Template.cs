using System;
using System.Text;
using Alba.CsConsoleFormat;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public abstract class Template
    {
        public AlbaContext Context { get; set; }

        protected Template(IContext context)
        {
            Context = new AlbaContext(context.ThrowIfArgumentNull(nameof(context)));
        }

        public abstract Document Create();
    }
}
