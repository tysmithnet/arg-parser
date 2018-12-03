using System;
using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    ///     EventArgs for when a Parser is created
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ParserCreatedEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ParserCreatedEventArgs" /> class.
        /// </summary>
        /// <param name="parser">The parser.</param>
        public ParserCreatedEventArgs(Parser parser)
        {
            Parser = parser.ThrowIfArgumentNull(nameof(parser));
        }

        /// <summary>
        ///     Gets or sets the parser.
        /// </summary>
        /// <value>The parser.</value>
        public Parser Parser { get; set; }
    }
}