using System.Collections.Generic;

namespace ArgParser.Core.Help
{
    public interface IHelpNode
    {
        void Accept(IHelpNodeVisitor visitor);
    }

    public class RootNode : IHelpNode
    {
        public IReadOnlyList<IHelpNode> Children { get; protected internal set; }

        /// <inheritdoc />
        public void Accept(IHelpNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
