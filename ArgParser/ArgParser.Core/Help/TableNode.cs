using System.Collections.Generic;

namespace ArgParser.Core.Help
{
    public class TableNode : IHelpNode
    {
        /// <inheritdoc />
        public virtual void Accept(IHelpNodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public IReadOnlyList<TableRowNode> Rows { get; set; }
    }
}