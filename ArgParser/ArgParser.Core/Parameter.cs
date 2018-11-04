namespace ArgParser.Core
{
    public abstract class Parameter : IConsumer
    {
        public Parser Parent { get; protected internal set; }
        public abstract IterationInfo CanConsume(object instance, IterationInfo info);
        public bool HasBeenConsumed { get; protected internal set; }
        public abstract IterationInfo Consume(object instance, ConsumptionRequest request);
        public int MinRequired { get; protected internal set; } = 1;
        public int MaxRequired { get; protected internal set; } = int.MaxValue;
    }
}