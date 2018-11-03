using System.Collections.Generic;

namespace ArgParser.Core.Help.Dom
{
    public class RootNode : HelpNode
    {
        public override void Accept(IHelpNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}