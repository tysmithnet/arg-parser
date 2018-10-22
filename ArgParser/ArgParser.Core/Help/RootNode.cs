using System.Collections.Generic;

namespace ArgParser.Core.Help
{
    public class RootNode : IHelpNode
    {
        /// <inheritdoc />
        public void Accept(IHelpNodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public IReadOnlyList<IHelpNode> Children { get; protected internal set; }
    }
}