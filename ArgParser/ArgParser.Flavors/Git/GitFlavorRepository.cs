using System;
using System.Collections.Generic;

namespace ArgParser.Flavors.Git
{
    public class GitFlavorRepository : IGitFlavorRepository
    {
        protected internal class Node
        {
            public string Name { get; set; }
            public GitFlavor Flavor { get; set; }
            public Node Parent { get; set; }
            public IList<Node> Children { get; set; }

            /// <inheritdoc />
            public Node(string name, GitFlavor flavor)
            {
                Name = name ?? throw new ArgumentNullException(nameof(name));
                Flavor = flavor ?? throw new ArgumentNullException(nameof(flavor));
            }
        }

        protected internal Dictionary<string, Node> Nodes { get; set; } = new Dictionary<string, Node>();
        
        protected internal Dictionary<string, List<string>> ParentChildRelationships = new Dictionary<string, List<string>>();

        /// <inheritdoc />
        public GitFlavor Create(string name)
        {
            if(Nodes.ContainsKey(name))
                throw new ArgumentException($"There is already a GitFlavor with name={name}. Please choose another name");
            var newGuy = new GitFlavor(name);
            var node = new Node(name, newGuy);
            Nodes.Add(name, node);
            return newGuy;
        }

        /// <inheritdoc />
        public GitFlavor Get(string name)
        {
            if(!Nodes.ContainsKey(name))
                throw new KeyNotFoundException($"Cannot find GitFlavor with name={name}, are you sure it's added?");
            return Nodes[name].Flavor;
        }

        public GitFlavor GetParent(string name, bool recursive)
        {
            if (!Nodes.ContainsKey(name))
                throw new KeyNotFoundException($"Cannot find GitFlavor with name={name}, are you sure it's added?");
            var node = Nodes[name];
            return node.Parent?.Flavor;
        }

        public IEnumerable<GitFlavor> GetChildren(string name, bool recursive)
        {
            return null;
        }

        public void EstablishParentChildRelationship(string parent, string child)
        {
            if(!Nodes.ContainsKey(parent))
                throw new KeyNotFoundException($"Cannot find parent by name={parent}, are you sure it exists?");
            if(!Nodes.ContainsKey(child))
                throw new KeyNotFoundException($"Cannot find child by name={child}, are you sure it exists?");
            var parentNode = Nodes[parent];
            var childNode = Nodes[child];
            childNode.Parent = parentNode;
            if (parentNode.Children.Contains(childNode))
                return;
            parentNode.Children.Add(childNode);
        }
    }
}