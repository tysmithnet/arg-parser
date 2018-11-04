namespace ArgParser.Core.Validation
{
    public class NoFactoryFunctionError : ParseError
    {
        public NoFactoryFunctionError()
        {
            Message = "No factory functions were provided";
        }
    }
}
