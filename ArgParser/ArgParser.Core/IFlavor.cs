using System;
using System.Collections.Generic;
using System.Text;

namespace ArgParser.Core
{
    public interface IFlavor
    {
        IParseResult Parse(string[] args);
    }
}
