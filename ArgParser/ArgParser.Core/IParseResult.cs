﻿using System;
using System.Collections.Generic;

namespace ArgParser.Core
{
    public interface IParseResult
    {
        void When<T>(Action<T> handler);
        void WhenError(Action<IEnumerable<ParseException>> handler);
    }
}