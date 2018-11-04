namespace ArgParser.Core
{
    public interface IConsumer
    {
        IterationInfo CanConsume(object instance, IterationInfo info);
        IterationInfo Consume(object instance, ConsumptionRequest request);
    }

    public class ConsumptionRequest
    {
        public IterationInfo Info { get; protected internal set; }
        public int Max { get; protected internal set; }
    }
}