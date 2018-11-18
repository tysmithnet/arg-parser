using System;
using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles.ParseStrategy
{
    public class MutateArgsRequest
    {
        public string[] Args { get; set; }
        public IList<Parser> Chain { get; set; }
        public IContext Context { get; set; }

        public MutateArgsRequest(string[] args, IEnumerable<Parser> chain, IContext context)
        {
            Args = args.ThrowIfArgumentNull(nameof(args));
            Chain = chain.ThrowIfArgumentNull(nameof(chain)).ToList();
            Context = context.ThrowIfArgumentNull(nameof(context));
        }
    }
}