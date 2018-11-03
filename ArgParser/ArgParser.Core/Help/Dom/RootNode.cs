using System.Collections.Generic;

namespace ArgParser.Core.Help.Dom
{
    public class RootNode : IHelpNode
    {
            
        public void Accept(IHelpNodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public IReadOnlyList<IHelpNode> Children { get; protected internal set; }
    }
}