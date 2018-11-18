using System.Collections.Generic;
using ArgParser.Core;
using ArgParser.Core.Extensions;

namespace ArgParser.Styles
{
    public class HierarchyNode
    {
        public HierarchyNode(string id)
        {
            Id = id.ThrowIfArgumentNull(nameof(id));
        }

        public void Add(HierarchyNode child)
        {
            Children.Add(child);
            child.Parent = this;
        }

        public IList<HierarchyNode> Children { get; set; } = new List<HierarchyNode>();
        public string Id { get; set; }
        public HierarchyNode Parent { get; set; }
    }
}