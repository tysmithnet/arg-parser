using System.Collections.Generic;

namespace ArgParser.Core.Help
{
    public class TableRowNode : IHelpNode
    {
        /// <inheritdoc />
        public virtual void Accept(IHelpNodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public IList<TableDataNode> TableDataNodes { get; set; } = new List<TableDataNode>();
    }
}