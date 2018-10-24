using System;
using System.Collections.Generic;
using System.Text;

namespace ArgParser.Core
{
    public interface IFlavor : ILexer, IParser, IParseStrategy
    {

    }
}
