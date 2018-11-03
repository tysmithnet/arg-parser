using System;
using System.Collections.Generic;
using System.Text;

namespace ArgParser.Core.Help.Dom
{
    public abstract class ListNode : HelpNode
    {
        
    }

    public class OrderedListNode : ListNode
    {
        public override void Accept(IHelpNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class UnOrderedListNode : ListNode
    {
        public override void Accept(IHelpNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
