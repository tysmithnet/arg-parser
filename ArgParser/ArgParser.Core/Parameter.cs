namespace ArgParser.Core
{
    public abstract class Parameter : IConsumer
    {
        public abstract bool CanConsume(object instance, IterationInfo info);
        public abstract bool HasBeenConsume { get; }
        public abstract IterationInfo Consume(object instance, IterationInfo info);
    }
}