namespace ArgParser.Core
{
    public class InvalidProgressException : ParseException
    {
        public InvalidProgressException(string message) : base(message)
        {
        }
    }
}