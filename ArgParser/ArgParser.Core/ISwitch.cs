namespace ArgParser.Core
{
    public interface ISwitch<in T>
    {
        CanHandleCallback<T> CanHandle { get; }
        HandlerCallback<T> Handle { get; }
    }
}