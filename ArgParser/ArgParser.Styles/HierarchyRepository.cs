using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles
{
    public class HierarchyRepository : IHierarchyRepository
    {
        public void AddParser(string parserId)
        {
            if (Nodes.ContainsKey(parserId))
                return;
            Nodes.Add(parserId, new Node(parserId));
        }

        public void EstablishParentChildRelationship(string parentParserId, string childParserId)
        {
            parentParserId.ThrowIfArgumentNull(nameof(parentParserId));
            childParserId.ThrowIfArgumentNull(nameof(childParserId));
            if (!Nodes.ContainsKey(parentParserId))
                throw new KeyNotFoundException(
                    $"Unable to find parent parser with id={parentParserId}, are you sure it was added and you are using the correct id?");
            if (!Nodes.ContainsKey(childParserId))
                throw new KeyNotFoundException(
                    $"Unable to find child parser with id={childParserId}, are you sure it was added and you are using the correct id?");
            var parent = Nodes[parentParserId];
            if (parent.Children.All(x => x.Id != childParserId))
                parent.Add(Nodes[childParserId]);
        }

        public IEnumerable<string> GetAncestors(string parserId)
        {
            parserId.ThrowIfArgumentNull(nameof(parserId));
            if (!Nodes.ContainsKey(parserId))
                throw new KeyNotFoundException(
                    $"Unable to find parser with id={parserId}, are you sure it was added and you are using the correct id?");
            var results = new List<string>();
            var itr = Nodes[parserId];
            while (itr.Parent != null)
            {
                results.Add(itr.Parent.Id);
                itr = itr.Parent;
            }

            return results;
        }

        public IEnumerable<string> GetChildren(string parserId)
        {
            if (!Nodes.ContainsKey(parserId))
                throw new KeyNotFoundException(
                    $"Unable to find parent parser with id={parserId}, are you sure it was added and you are using the correct id?");
            return Nodes[parserId].Children.Select(x => x.Id);
        }

        public string GetRoot()
        {
            var allChildren = Nodes.SelectMany(pair => pair.Value.Children);
            var root = Nodes.Values.Except(allChildren).Single();
            return root.Id;
        }

        public bool IsParent(string parentParserId, string childParserId)
        {
            if (childParserId == null)
                return false;
            if (!Nodes.ContainsKey(childParserId))
                return false;
            return Nodes[childParserId].Parent?.Id == parentParserId;
        }

        protected internal Dictionary<string, Node> Nodes { get; set; } = new Dictionary<string, Node>();

        protected internal class Node
        {
            public Node(string id)
            {
                Id = id.ThrowIfArgumentNull(nameof(id));
            }

            public void Add(Node child)
            {
                Children.Add(child);
                child.Parent = this;
            }

            public IList<Node> Children { get; set; } = new List<Node>();
            public string Id { get; set; }
            public Node Parent { get; set; }
        }
    }
}