namespace ArgParser.Core
{
    public class MissingValueException : ParseException
    {
        public MissingValueException(string message) : base(message)
        {
        }
    }
}