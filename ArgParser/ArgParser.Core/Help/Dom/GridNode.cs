using System;
using System.Collections.Generic;
using System.Text;

namespace ArgParser.Core.Help.Dom
{
    public class GridNode : HelpNode
    {
        public int NumColumns { get; set; } = 1;

        /// <inheritdoc />
        public override T Accept<T>(IHelpNodeVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
