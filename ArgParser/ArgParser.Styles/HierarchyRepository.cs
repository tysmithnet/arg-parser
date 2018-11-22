// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-12-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="HierarchyRepository.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    ///     Default hierarchy repository that allows only parent-child relationships
    /// </summary>
    /// <seealso cref="ArgParser.Core.IHierarchyRepository" />
    public class HierarchyRepository : IHierarchyRepository
    {
        /// <summary>
        ///     Adds the parser.
        /// </summary>
        /// <param name="parserId">The parser identifier.</param>
        public void AddParser(string parserId)
        {
            if (Nodes.ContainsKey(parserId))
                return;
            Nodes.Add(parserId, new HierarchyNode(parserId));
        }

        /// <summary>
        ///     Establishes a parent child relationship between parsers.
        /// </summary>
        /// <param name="parentParserId">The parent parser identifier.</param>
        /// <param name="childParserId">The child parser identifier.</param>
        /// <exception cref="KeyNotFoundException">
        /// </exception>
        public virtual void EstablishParentChildRelationship(string parentParserId, string childParserId)
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

        /// <summary>
        ///     Gets the ancestors of a parser
        /// </summary>
        /// <param name="parserId">The parser identifier.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public virtual IEnumerable<string> GetAncestors(string parserId)
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

        /// <summary>
        ///     Gets the children id's of a parser
        /// </summary>
        /// <param name="parserId">The parser identifier.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public virtual IEnumerable<string> GetChildren(string parserId)
        {
            if (!Nodes.ContainsKey(parserId))
                throw new KeyNotFoundException(
                    $"Unable to find parent parser with id={parserId}, are you sure it was added and you are using the correct id?");
            return Nodes[parserId].Children.Select(x => x.Id);
        }

        /// <summary>
        ///     Gets the root parser
        /// </summary>
        /// <returns>The root parser id</returns>
        public virtual string GetRoot()
        {
            var allChildren = Nodes.SelectMany(pair => pair.Value.Children);
            var root = Nodes.Values.Except(allChildren).Single();
            return root.Id;
        }

        /// <summary>
        ///     Determines whether the specified parent parser identifier is parent.
        /// </summary>
        /// <param name="parentParserId">The parent parser identifier.</param>
        /// <param name="childParserId">The child parser identifier.</param>
        /// <returns><c>true</c> if the specified parent parser identifier is parent; otherwise, <c>false</c>.</returns>
        public virtual bool IsParent(string parentParserId, string childParserId)
        {
            if (childParserId == null)
                return false;
            if (!Nodes.ContainsKey(childParserId))
                return false;
            return Nodes[childParserId].Parent?.Id == parentParserId;
        }

        /// <summary>
        ///     Gets or sets the nodes.
        /// </summary>
        /// <value>The nodes.</value>
        protected internal Dictionary<string, HierarchyNode> Nodes { get; set; } =
            new Dictionary<string, HierarchyNode>();
    }
}