namespace ArgParser.Core
{
    public interface IConsumer
    {
        ConsumptionResult CanConsume(object instance, IterationInfo info);
        ConsumptionResult Consume(object instance, ConsumptionRequest request);
        void Reset();
    }

    public interface IConsumer<in T> : IConsumer
    {
        ConsumptionResult CanConsume(T instance, IterationInfo info);
        ConsumptionResult Consume(T instance, ConsumptionRequest request);
    }
}