namespace ArgParser.Core
{
    public class NoFactoryFunctionException : ParseException
    {
        public NoFactoryFunctionException(string message) : base(message)
        {
        }
    }
}