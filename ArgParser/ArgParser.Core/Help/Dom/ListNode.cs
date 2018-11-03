using System;
using System.Collections.Generic;
using System.Text;

namespace ArgParser.Core.Help.Dom
{
    public class ListNode : HelpNode
    {
        public override T Accept<T>(IHelpNodeVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }

}
