using System;
using System.Collections.Generic;
using System.Text;

namespace ArgParser.Core
{
    public class NoFactoryFunctionError : ParseError
    {
        public NoFactoryFunctionError()
        {
            Message = "No factory functions were provided";
        }
    }
}
