namespace ArgParser.Core
{
    public interface IConsumer
    {
        ConsumptionResult CanConsume(object instance, IterationInfo info);
        ConsumptionResult Consume(object instance, ConsumptionRequest request);
    }
}