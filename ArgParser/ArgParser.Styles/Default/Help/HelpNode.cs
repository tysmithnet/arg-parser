using System.Collections.Generic;

namespace ArgParser.Styles.Default.Help
{
    public abstract class HelpNode
    {
        public virtual T Accept<T>(IHelpNodeVisitor<T> visitor) => visitor.Visit(this);

        public void AddChild(HelpNode child)
        {
            Children.Add(child);
        }

        public IList<HelpNode> Children { get; protected internal set; } = new List<HelpNode>();
    }
}