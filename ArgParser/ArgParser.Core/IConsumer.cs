namespace ArgParser.Core
{
    public interface IConsumer
    {
        IterationInfo CanConsume(object instance, IterationInfo info);
        IterationInfo Consume(object instance, ConsumptionRequest request);
    }
}