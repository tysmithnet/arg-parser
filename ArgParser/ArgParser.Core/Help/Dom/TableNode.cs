using System.Collections.Generic;

namespace ArgParser.Core.Help.Dom
{
    public class TableNode : HelpNode
    {
        public override void Accept(IHelpNodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override T Accept<T>(IHelpNodeVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public int NumColumns { get; set; }
        public new 
    }
}