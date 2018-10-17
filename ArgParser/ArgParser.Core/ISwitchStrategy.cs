namespace ArgParser.Core
{
    public interface ISwitchStrategy<T> : IParsingStrategy<T>
    {
        IterationInfo ConsumeSwitch(T instasnce, IterationInfo info);
        bool IsSwitch(IterationInfo info);
        bool IsGroup(IterationInfo info);
        IterationInfo ConsumeGroup(T instance, IterationInfo info);
    }
}