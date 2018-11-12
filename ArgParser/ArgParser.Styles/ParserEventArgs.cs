using System;
using ArgParser.Core;

namespace ArgParser.Styles
{
    public abstract class ParserEventArgs : EventArgs
    {
        public IContext Context { get; protected internal set; }

        protected ParserEventArgs(IContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}