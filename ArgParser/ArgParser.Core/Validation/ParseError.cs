﻿namespace ArgParser.Core.Validation
{
    public abstract class ParseError
    {
        public string Message { get; set; }
        public abstract bool DoesStopProcessing { get; }
    }
}