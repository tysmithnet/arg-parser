using System;
using ArgParser.Core;

namespace ArgParser.Flavors.Git
{
    public class BooleanSwitch : Switch
    {
        /// <inheritdoc />
        public override IIterationInfo Consume(object instance, IIterationInfo info)
        {
            ConsumeCallback(instance);
            return info.Consume(1);
        }

        public Action<object> ConsumeCallback { get; set; }
    }
}