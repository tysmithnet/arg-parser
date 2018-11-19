// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="HierarchyNode.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    /// Represents a parser in the global relationship pool of parsers
    /// </summary>
    public class HierarchyNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HierarchyNode"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public HierarchyNode(string id)
        {
            Id = id.ThrowIfArgumentNull(nameof(id));
        }

        /// <summary>
        /// Adds the specified child.
        /// </summary>
        /// <param name="child">The child.</param>
        public void Add(HierarchyNode child)
        {
            Children.Add(child);
            child.Parent = this;
        }

        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        /// <value>The children.</value>
        public IList<HierarchyNode> Children { get; protected internal set; } = new List<HierarchyNode>();
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; protected internal set; }
        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>The parent.</value>
        public HierarchyNode Parent { get; protected internal set; }
    }
}