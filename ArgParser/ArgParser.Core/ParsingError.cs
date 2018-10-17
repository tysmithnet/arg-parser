using System;

namespace ArgParser.Core
{
    public class ParsingError
    {
        public string Message { get; set; }

        /// <inheritdoc />
        public ParsingError(string message)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }
    }
}