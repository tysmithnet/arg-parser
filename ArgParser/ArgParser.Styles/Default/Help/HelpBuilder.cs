using System;
using ArgParser.Core;

namespace ArgParser.Styles.Default.Help
{
    public class HelpBuilder
    {
        protected internal ParserBuilder ParserBuilder { get; set; }

        /// <inheritdoc />
        public HelpBuilder(ParserBuilder parserBuilder)
        {
            ParserBuilder = parserBuilder.ThrowIfArgumentNull(nameof(parserBuilder));
        }
    }
}
