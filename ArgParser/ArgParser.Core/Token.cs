using System;

namespace ArgParser.Core
{
    public class Token : IToken
    {
        /// <inheritdoc />
        public Token(string raw)
        {
            Raw = raw ?? throw new ArgumentNullException(nameof(raw));
        }

        /// <inheritdoc />
        public string Raw { get; }
    }
}