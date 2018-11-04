using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class MissingValueException : ParseException
    {
        public MissingValueException(string message) : base(message)
        {
        }
    }
}