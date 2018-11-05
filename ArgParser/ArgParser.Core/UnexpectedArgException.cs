using System;
using System.Collections.Generic;
using System.Text;

namespace ArgParser.Core
{
    public class UnexpectedArgException : ParseException
    {
        public UnexpectedArgException(string message) : base(message)
        {
        }
    }
}
