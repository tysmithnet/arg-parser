using System;
using System.Diagnostics;
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
            return new ConsumptionResult(request.Info, values.Length, this);
        }

        public Action<object, string[]> ConsumeCallback { get; protected internal set; }
        public bool HasBeenConsumed { get; protected internal set; }
        public int MaxAllowed { get; protected internal set; } = int.MaxValue;
        public int MinRequired { get; protected internal set; } = 1;
        public Parser Parent { get; protected internal set; }
    }

    public abstract class Parameter<T> : Parameter
    {
        private static Action<object, string[]> Convert(Action<T, string[]> toConvert)
        {
            // todo: does this belong here?
            toConvert.ThrowIfArgumentNull(nameof(toConvert));
            return (instance, strings) =>
            {
                instance.ThrowIfArgumentNull(nameof(instance));
                if (instance is T casted)
                    toConvert(casted, strings);
                else
                    throw new ArgumentException($"Expected to find object of type={typeof(T).FullName}, but found type={instance.GetType().FullName}");
            };
        }

        protected Parameter(Action<T, string[]> consumeCallback) : base(Convert(consumeCallback))
        {
        }
    }
}