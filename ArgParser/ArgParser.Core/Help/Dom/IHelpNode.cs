using System.Collections.Generic;

namespace ArgParser.Core.Help.Dom
{
    public interface IHelpNode
    {
        T Accept<T>(IHelpNodeVisitor<T> visitor);
        IEnumerable<IHelpNode> Children { get; }
        void Add(IHelpNode node);
        void Remove(IHelpNode node);
    }
}