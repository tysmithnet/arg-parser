namespace ArgParser.Core
{
    public abstract class Parameter : IConsumer
    {
        public abstract bool CanConsume(object instance, IterationInfo info);

        public abstract IterationInfo Consume(object instance, IterationInfo info);
    }
}