using System.Collections.Generic;

namespace ArgParser.Core.Help.Dom
{
    public class RootNode : HelpNode
    {
        public override T Accept<T>(IHelpNodeVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}