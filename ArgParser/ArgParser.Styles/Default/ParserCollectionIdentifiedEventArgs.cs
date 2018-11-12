using System.Collections.Generic;
using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class ParserCollectionIdentifiedEventArgs : ParserEventArgs
    {
        public IEnumerable<Parser> Parsers { get; protected internal set; }

        public ParserCollectionIdentifiedEventArgs(IEnumerable<Parser> parsers, IContext context) : base(context)
        {
            Parsers = parsers.ThrowIfArgumentNull(nameof(parsers));
            Context = context.ThrowIfArgumentNull(nameof(context));
        }
    }
}