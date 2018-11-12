using System;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class Positional : Parameter, IRequirable
    {
        public Positional(Parser parent, Action<object, string[]> consumeCallback, int min = 1,
            int max = int.MaxValue) : base(parent,
            consumeCallback)
        {
            MinRequired = min;
            MaxAllowed = max;
        }

        public override ConsumptionResult CanConsume(object instance, IterationInfo info)
        {
            if (HasBeenConsumed)
                return new ConsumptionResult(info, 0, null);
            var values = info.FromNowOn().Take(MaxAllowed).ToArray();
            return new ConsumptionResult(info, values.Length, this);
        }

        public bool IsRequired { get; protected internal set; }
    }

    public class Positional<T> : Positional
    {
        public Positional(Parser parent, Action<T, string[]> consumeCallback, int min = 1, int max = int.MaxValue) :
            base(parent,
                Convert(consumeCallback), min, max)
        {
        }

        private static Action<object, string[]> Convert(Action<T, string[]> action)
        {
            return (instance, s) =>
            {
                if (instance is T casted)
                    action(casted, s);
                else
                    throw new ArgumentException(
                        $"Expected to find object of type={typeof(T).FullName}, but found type={instance.GetType().FullName}");
            };
        }
    }
}