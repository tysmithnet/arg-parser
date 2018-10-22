using System.Collections.Generic;

namespace ArgParser.Core.Help
{
    public class TableDataNode : IHelpNode
    {
        public IList<TableRowNode> TableRowNodes { get; set; } = new List<TableRowNode>();
        /// <inheritdoc />
        public virtual void Accept(IHelpNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}