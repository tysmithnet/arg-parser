using System.Collections.Generic;

namespace ArgParser.Core.Help.Dom
{
    public class TableRowNode : HelpNode
    {
            
        public override void Accept(IHelpNodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public IList<TableDataNode> TableDataNodes { get; set; } = new List<TableDataNode>();
    }
}