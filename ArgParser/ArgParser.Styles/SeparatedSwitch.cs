using System;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles
{
    public class SeparatedSwitch : Switch
    {
        static Action<object, string[]> Convert(Action<object, string> callback)
        {
            return (o, s) => { callback(o, s.First()); };
        }

        public string Separator { get; set; } = "=";

        public override ConsumptionResult CanConsume(object instance, IterationInfo info)
        {
            if(Letter.HasValue && info.Current.StartsWith($"{LetterToken}{Letter}{Separator}"))
                return new ConsumptionResult(info, 1, this);
            if(Word.IsNotNullOrWhiteSpace() && info.Current.StartsWith($"{WordToken}{Word}{Separator}"))
                return new ConsumptionResult(info, 1, this);
            return new ConsumptionResult(info, 0, this);
        }

        public SeparatedSwitch(Parser parent, char? letter, string word, Action<object, string> consumeCallback) : base(parent, letter, word, Convert(consumeCallback))
        {
            MinRequired = 1;
            MaxAllowed = 1;
        }

        public override ConsumptionResult Consume(object instance, ConsumptionRequest request)
        {
            var index = request.Info.Current.IndexOf(Separator, StringComparison.Ordinal);
            var value = request.Info.Current.Substring(index + 1);
            ConsumeCallback(instance, new[] {value});
            return new ConsumptionResult(request.Info, 1, this);
        }
    }
    public class SeparatedSwitch<T> : SeparatedSwitch
    {
        public SeparatedSwitch(Parser parent, char? letter, string word, Action<T, string> consumeCallback) : base(parent, letter, word, consumeCallback.ToNonGenericAction())
        {

        }
    }
}