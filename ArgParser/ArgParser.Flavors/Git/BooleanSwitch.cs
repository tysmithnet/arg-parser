﻿using System;
using ArgParser.Core;

namespace ArgParser.Flavors.Git
{
    public class BooleanSwitch : Switch
    {
        public override IIterationInfo Consume(object instance, IIterationInfo info)
        {
            ConsumeCallback(instance);
            HasBeenConsumed = true;
            return info.Consume(1);
        }

        public Action<object> ConsumeCallback { get; set; }

        public override bool HasBeenConsumed { get; set; }
    }
}