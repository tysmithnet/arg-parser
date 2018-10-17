namespace ArgParser.Core
{
    public class FormatError : ParsingError
    {
        /// <inheritdoc />
        public FormatError(string message) : base(message)
        {
        }
    }
}