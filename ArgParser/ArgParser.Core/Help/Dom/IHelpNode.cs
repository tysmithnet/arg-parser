﻿using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core.Help.Dom
{
    public interface IHelpNode
    {
        void Accept(IHelpNodeVisitor visitor);
        IEnumerable<IHelpNode> Children { get; }
        void Add(IHelpNode node);
        void Remove(IHelpNode node);
    }

    public abstract class HelpNode : IHelpNode
    {
        protected internal IList<IHelpNode> ChildrenInternal { get; set; } = new List<IHelpNode>();

        public abstract void Accept(IHelpNodeVisitor visitor);

        public IEnumerable<IHelpNode> Children => ChildrenInternal.ToList();

        public virtual void Add(IHelpNode node)
        {
            if (ChildrenInternal.Contains(node))
                return;
            ChildrenInternal.Add(node);
        }

        public virtual void Remove(IHelpNode node)
        {
            ChildrenInternal.Remove(node);
        }
    }
}