namespace ArgParser.Core
{
    public interface ISubCommandStrategy<T> : IParsingStrategy<T>
    {
        bool IsSubCommand(IterationInfo info);
        ParseResult Parse(IterationInfo info);
    }
}