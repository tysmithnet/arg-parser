namespace ArgParser.Core
{
    public interface IConsumer
    {
        bool CanConsume(object instance, IterationInfo info);
        IterationInfo Consume(object instance, IterationInfo info);
    }
}