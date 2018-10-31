using System;
using ArgParser.Core;

namespace ArgParser.Flavors.Git
{
    public class SingleValueSwitch : Switch
    {
        /// <inheritdoc />
        public override IIterationInfo Consume(object instance, IIterationInfo info)
        {
            ConsumeCallback(instance, info.Next.Raw); // todo: check
            HasBeenConsumed = true;
            return info.Consume(2);
        }

        public Action<object, string> ConsumeCallback { get; set; }

        /// <inheritdoc />
        public override bool HasBeenConsumed { get; set; }
    }
}