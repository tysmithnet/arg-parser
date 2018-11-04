namespace ArgParser.Core
{
    public abstract class Parameter : IConsumer
    {
        public abstract bool CanConsume(object instance, IterationInfo info);
        public bool HasBeenConsumed { get; protected internal set; }
        public abstract IterationInfo Consume(object instance, IterationInfo info);
    }
}