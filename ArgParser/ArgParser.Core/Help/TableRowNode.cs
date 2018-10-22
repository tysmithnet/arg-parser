using System.Collections.Generic;

namespace ArgParser.Core.Help
{
    public class TableRowNode : IHelpNode
    {
        public IList<TableDataNode> TableDataNodes { get; set; } = new List<TableDataNode>();

        /// <inheritdoc />
        public virtual void Accept(IHelpNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}