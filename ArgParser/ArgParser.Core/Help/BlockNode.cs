using System;
using System.Collections.Generic;
using System.Text;

namespace ArgParser.Core.Help
{
    public class BlockNode : HelpNode
    {
    }

    public class GridNode : HelpNode
    {
        public int Columns { get; set; } = 1;
    }
}
