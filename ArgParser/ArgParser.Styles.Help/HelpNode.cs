using System.Collections.Generic;

namespace ArgParser.Styles.Help
{
    public abstract class HelpNode
    {
        public virtual T Accept<T>(IHelpNodeVisitor<T> visitor) => visitor.Visit(this);

        public void AddChild(HelpNode child)
        {
            Children.Add(child);
        }

        public IEnumerable<string> Classes { get; set; }
        protected internal List<string> ClassesInternal { get; set; }

        public virtual void AddClass(string @class)
        {
            if(!ClassesInternal.Contains(@class))
                ClassesInternal.Add(@class);
        }

        public IList<HelpNode> Children { get; protected internal set; } = new List<HelpNode>();
    }
}