using System.Collections.Generic;

namespace ArgParser.Core.Help
{
    public class TableNode : IHelpNode
    {
        public IReadOnlyList<TableRowNode> Rows { get; set; }

        /// <inheritdoc />
        public virtual void Accept(IHelpNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}