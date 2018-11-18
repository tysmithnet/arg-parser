using System;
using ArgParser.Core;

namespace ArgParser.Styles.ParseStrategy
{
    public class ChainIdentificationRequest
    {
        public string[] Args { get; set; }
        public IContext Context { get; set; }

        public ChainIdentificationRequest(string[] args, IContext context)
        {
            Args = args ?? throw new ArgumentNullException(nameof(args));
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}