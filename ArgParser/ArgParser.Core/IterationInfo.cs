using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArgParser.Core
{
    public class IterationInfo
    {
        public string[] AllArgs { get; set; }
        public int Index { get; internal set; }
        public string Cur => AllArgs[Index];
        public string[] Rest => AllArgs.Skip(Index + 1).ToArray();
    }
}
