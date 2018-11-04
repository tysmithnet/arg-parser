using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core.Help.Dom
{
    public abstract class HelpNode : IHelpNode
    {
        public abstract T Accept<T>(IHelpNodeVisitor<T> visitor);

        public IEnumerable<IHelpNode> Children => ChildrenInternal.ToList();

        public virtual void Add(IHelpNode child)
        {
            ChildrenInternal.Add(child);
        }

        protected internal List<IHelpNode> ChildrenInternal { get; set; } = new List<IHelpNode>();
    }
}