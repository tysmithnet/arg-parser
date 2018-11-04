namespace ArgParser.Core
{
    public abstract class Parameter : IConsumer
    {
        public Parser Parent { get; protected internal set; }
        public abstract IterationInfo CanConsume(object instance, IterationInfo info);
        public bool HasBeenConsumed { get; protected internal set; }
        public abstract IterationInfo Consume(object instance, ConsumptionRequest request);
    }
}