using System;
using System.Text;
using Alba.CsConsoleFormat;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public abstract class Template
    {
        protected internal IContext Context { get; set; }

        protected Template(IContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public abstract Document Create();
    }
}
