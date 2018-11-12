using System;
using ArgParser.Core;

namespace ArgParser.Styles
{
    public class InstanceCreatedEventArgs : ParserEventArgs
    {
        public object Instance { get; protected internal set; }
        public Parser Creator { get; protected internal set; }
        public InstanceCreatedEventArgs(object instance, Parser creator, IContext context) : base(context) // todo: get rid of context here somehow, and for alll of them
        {
            Instance = instance ?? throw new ArgumentNullException(nameof(instance));
            Creator = creator.ThrowIfArgumentNull(nameof(creator));
            Context = context.ThrowIfArgumentNull(nameof(context));
        }
    }
}