namespace ArgParser.Core
{
    public interface IParseStrategy
    {
        IParseResult Parse(string[] args, IContext context);
    }
}