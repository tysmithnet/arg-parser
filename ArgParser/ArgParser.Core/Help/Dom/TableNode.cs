using System.Collections.Generic;

namespace ArgParser.Core.Help.Dom
{
    public class TableNode : IHelpNode
    {
            
        public virtual void Accept(IHelpNodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public IReadOnlyList<TableRowNode> Rows { get; set; }
    }
}