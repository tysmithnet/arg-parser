namespace ArgParser.Core
{
    public class CardinalityError : ParsingError
    {
        /// <inheritdoc />
        public CardinalityError(string message) : base(message)
        {
        }
    }
}