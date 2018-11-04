using System;
using System.Collections.Generic;
using System.Text;

namespace ArgParser.Core.Help
{
    public abstract class HelpNode
    {
        public IList<HelpNode> Children { get; protected internal set; } = new List<HelpNode>();

        public void AddChild(HelpNode child)
        {
            Children.Add(child);
        }
    }
}
