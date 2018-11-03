﻿using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core.Help.Dom
{
    public interface IHelpNode
    {
        T Accept<T>(IHelpNodeVisitor<T> visitor);
        IEnumerable<IHelpNode> Children { get; }
        void Add(IHelpNode child);
    }

    public abstract class HelpNode : IHelpNode
    {
        public abstract T Accept<T>(IHelpNodeVisitor<T> visitor);

        public IEnumerable<IHelpNode> Children => ChildrenInternal.ToList();

        /// <inheritdoc />
        public void Add(IHelpNode child)
        {
            ChildrenInternal.Add(child);
        }

        protected internal List<IHelpNode> ChildrenInternal { get; set; }
        
    }
}