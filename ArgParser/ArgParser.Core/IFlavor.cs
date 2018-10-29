namespace ArgParser.Core
{
    public interface IFlavor
    {
        IParseResult Parse(string[] args);
    }
}