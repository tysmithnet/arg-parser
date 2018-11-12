using ArgParser.Core;

namespace ArgParser.Styles
{
    public class ParserIdentifedEventArgs : ParserEventArgs
    {
        public Parser Parser { get; protected internal set; }

        public ParserIdentifedEventArgs(Parser parser, IContext context) : base(context)
        {
            Parser = parser.ThrowIfArgumentNull(nameof(parser));
            Context = context.ThrowIfArgumentNull(nameof(context));
        }
    }
}