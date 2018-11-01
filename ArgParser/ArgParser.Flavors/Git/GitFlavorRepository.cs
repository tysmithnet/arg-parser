﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Flavors.Git
{
    public class GitFlavorRepository : IGitFlavorRepository
    {
        protected internal Dictionary<string, List<string>> ParentChildRelationships =
            new Dictionary<string, List<string>>();

        /// <inheritdoc />
        public GitParser Create(string name)
        {
            if (Nodes.ContainsKey(name))
                throw new ArgumentException(
                    $"There is already a GitFlavor with name={name}. Please choose another name");
            var newGuy = new GitParser(name);
            var node = new Node(name, newGuy);
            Nodes.Add(name, node);
            return newGuy;
        }

        public void EstablishParentChildRelationship(string parent, string child)
        {
            if (!Nodes.ContainsKey(parent))
                throw new KeyNotFoundException($"Cannot find parent by name={parent}, are you sure it exists?");
            if (!Nodes.ContainsKey(child))
                throw new KeyNotFoundException($"Cannot find child by name={child}, are you sure it exists?");
            var parentNode = Nodes[parent];
            var childNode = Nodes[child];
            childNode.Parent = parentNode;
            if (parentNode.Children.Contains(childNode))
                return;
            parentNode.Children.Add(childNode);
        }

        /// <inheritdoc />
        public GitParser Get(string name)
        {
            if (!Nodes.ContainsKey(name))
                throw new KeyNotFoundException($"Cannot find GitFlavor with name={name}, are you sure it's added?");
            return Nodes[name].Parser;
        }

        public IEnumerable<GitParser> GetAncestors(string name)
        {
            if (!Nodes.ContainsKey(name))
                throw new KeyNotFoundException($"Cannot find GitFlavor with name={name}, are you sure it's added?");
            var node = Nodes[name].Parent;
            var results = new List<GitParser>();
            while (node != null)
            {
                results.Add(node.Parser);
                node = node.Parent;
            }

            return results;
        }

        public IEnumerable<GitParser> GetChildren(string parent, bool recursive)
        {
            if (!Nodes.ContainsKey(parent))
                throw new KeyNotFoundException($"Cannot find parent by name={parent}, are you sure it exists?");
            var node = Nodes[parent];
            if (!recursive)
                return node.Children.Select(x => x.Parser);
            var results = new List<GitParser>();
            var queue = new Queue<Node>();
            foreach (var nodeChild in node.Children) queue.Enqueue(nodeChild);
            while (queue.Any())
            {
                var first = queue.Dequeue();
                foreach (var firstChild in first.Children) queue.Enqueue(firstChild);
                results.Add(first.Parser);
            }

            return results;
        }

        public GitParser GetParent(string name)
        {
            if (!Nodes.ContainsKey(name))
                throw new KeyNotFoundException($"Cannot find GitFlavor with name={name}, are you sure it's added?");
            var node = Nodes[name];
            return node.Parent?.Parser;
        }

        protected internal Dictionary<string, Node> Nodes { get; set; } = new Dictionary<string, Node>();

        protected internal class Node
        {
            /// <inheritdoc />
            public Node(string name, GitParser parser)
            {
                Name = name ?? throw new ArgumentNullException(nameof(name));
                Parser = parser ?? throw new ArgumentNullException(nameof(parser));
            }

            public IList<Node> Children { get; set; } = new List<Node>();
            public GitParser Parser { get; set; }
            public string Name { get; set; }
            public Node Parent { get; set; }
        }
    }
}