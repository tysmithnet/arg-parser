using System;
using System.Linq;

namespace ArgParser.Core
{
    public abstract class Parameter : IConsumer
    {
        protected Parameter(Action<object, string[]> consumeCallback)
        {
            ConsumeCallback = consumeCallback.ThrowIfArgumentNull(nameof(consumeCallback));
        }

        protected Parameter()
        {
        }

        public abstract ConsumptionResult CanConsume(object instance, IterationInfo info);

        public virtual ConsumptionResult Consume(object instance, ConsumptionRequest request)
        {
            HasBeenConsumed = true;
            var values = request.AllToBeConsumed().Take(MaxAllowed).ToArray();
            ConsumeCallback(instance, values);
            return new ConsumptionResult(request.Info, values.Length);
        }

        public Action<object, string[]> ConsumeCallback { get; protected internal set; }
        public bool HasBeenConsumed { get; protected internal set; }
        public int MaxAllowed { get; protected internal set; } = int.MaxValue;
        public int MinRequired { get; protected internal set; } = 1;
        public Parser Parent { get; protected internal set; }
    }
}