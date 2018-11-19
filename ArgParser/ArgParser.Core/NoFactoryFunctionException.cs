namespace ArgParser.Core
{
    public class NoFactoryFunctionException : ParseException
    {
        public NoFactoryFunctionException(Parser parser) : base($"No factory function set on parser={parser.Id}")
        {
            Parser = parser.ThrowIfArgumentNull(nameof(parser));
        }

        public Parser Parser { get; protected internal set; }
    }
}