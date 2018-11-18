﻿using System;
using ArgParser.Core;
using ArgParser.Core.Extensions;

namespace ArgParser.Styles
{
    public class BooleanSwitch : Switch
    {
        public BooleanSwitch(Parser parent, char? letter, string word, Action<object> consumeCallback) : base(parent,
            letter, word,
            (o, strings) => consumeCallback(o))
        {
            MinRequired = 1;
            MaxAllowed = 1;
        }
    }

    public class BooleanSwitch<T> : BooleanSwitch
    {
        public BooleanSwitch(Parser parent, char? letter, string word, Action<T> consumeCallback) : base(parent, letter,
            word,
            consumeCallback.ToNonGenericAction())
        {
        }
    }
}