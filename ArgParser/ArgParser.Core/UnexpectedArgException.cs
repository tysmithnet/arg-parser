namespace ArgParser.Core
{
    public class UnexpectedArgException : ParseException
    {
        public UnexpectedArgException(string message) : base(message)
        {
        }
    }
}