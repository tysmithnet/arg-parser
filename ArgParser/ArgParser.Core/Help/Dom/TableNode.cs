using System.Collections.Generic;

namespace ArgParser.Core.Help.Dom
{
    public class TableNode : HelpNode
    {
        public override void Accept(IHelpNodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public IReadOnlyList<TableRowNode> Rows { get; set; }
    }
}