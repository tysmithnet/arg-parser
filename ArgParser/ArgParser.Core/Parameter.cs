using System;
using System.Diagnostics;
using System.Linq;
using ArgParser.Core.Help;

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
            if (request.Max < MinRequired)
                throw new MissingValueException($"Switch {this} expected to have at least {MinRequired} values, but was told it can only have {request.Max}. Are you sure you passed enough values to satisfy the switch?");
            HasBeenConsumed = true;
            var values = request.AllToBeConsumed().Take(MaxAllowed).ToArray();
            ConsumeCallback(instance, values);
            return new ConsumptionResult(request.Info, values.Length, this);
        }

        public void Reset()
        {
            HasBeenConsumed = false;
        }

        public ParameterHelp Help { get; set; }
        public Action<object, string[]> ConsumeCallback { get; protected internal set; }
        public bool HasBeenConsumed { get; protected internal set; }
        public int MaxAllowed { get; set; } = int.MaxValue;
        public int MinRequired { get; set; } = 1;
        public Parser Parent { get; protected internal set; }
    }
}