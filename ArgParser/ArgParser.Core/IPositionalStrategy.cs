namespace ArgParser.Core
{
    public interface IPositionalStrategy<T> : IParsingStrategy<T>
    {
        IterationInfo Consume(T instasnce, IterationInfo info);
        bool IsPositional(IterationInfo info);
    }
}