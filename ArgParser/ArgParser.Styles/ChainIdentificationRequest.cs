using ArgParser.Core;
using ArgParser.Core.Extensions;

namespace ArgParser.Styles
{
    public class ChainIdentificationRequest
    {
        public string[] Args { get; set; }
        public IContext Context { get; set; }

        public ChainIdentificationRequest(string[] args, IContext context)
        {
            Args = args.ThrowIfArgumentNull(nameof(args));
            Context = context.ThrowIfArgumentNull(nameof(context));
        }
    }
}