using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class Positional : Parameter
    {
        public Positional(Action<object, string[]> consumeCallback, int min = 1, int max = int.MaxValue) : base(consumeCallback)
        {
            MinRequired = min;
            MaxAllowed = max;
        }

        public override ConsumptionResult CanConsume(object instance, IterationInfo info)
        {
            if(HasBeenConsumed)
                return new ConsumptionResult(info, 0);
            var values = info.FromNowOn().Take(MaxAllowed).ToArray();
            return new ConsumptionResult(info, values.Length);
        }
    }

    public class Positional<T> : Positional
    {
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

        public Positional(Action<T, string[]> consumeCallback, int min = 1, int max = int.MaxValue) : base(Convert(consumeCallback), min, max)
        {

        }
    }
}
