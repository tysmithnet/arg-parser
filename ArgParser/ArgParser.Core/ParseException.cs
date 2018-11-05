using System;

namespace ArgParser.Core
{
    public class ParseException : Exception
    {
        public ParseException(string message) : base(message)
        {
        }
    }
}