using System;
using System.Collections.Generic;
using System.Text;

namespace ArgParser.Core
{
    public class NoFactoryFunctionException : ParseException
    {
        public NoFactoryFunctionException(string message) : base(message)
        {
        }
    }
}
