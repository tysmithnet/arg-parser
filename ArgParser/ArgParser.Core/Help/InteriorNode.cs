using System.Collections.Generic;

namespace ArgParser.Core.Help
{
    public abstract class InteriorNode : Node
    {
        public IList<Node> Children { get; set; }
    }
}
