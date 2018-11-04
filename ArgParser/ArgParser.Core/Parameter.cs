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

        public Action<object, string[]> ConsumeCallback { get; protected internal set; }
        public Parser Parent { get; protected internal set; }
        public abstract IterationInfo CanConsume(object instance, IterationInfo info);
        public bool HasBeenConsumed { get; protected internal set; }

        public virtual IterationInfo Consume(object instance, ConsumptionRequest request)
        {
            HasBeenConsumed = true;
            var values = request.Rest().Take(MaxAllowed).ToArray();
            ConsumeCallback(instance, values);
            return request.Info.Consume(values.Length);
        }
        public int MinRequired { get; protected internal set; } = 1;
        public int MaxAllowed { get; protected internal set; } = int.MaxValue;
    }
}