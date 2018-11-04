namespace ArgParser.Core.Validation
{
    public class NoFactoryFunctionException : ParseException
    {
        public NoFactoryFunctionException()
        {
            Message = "No factory functions were provided";
        }
    }
}
