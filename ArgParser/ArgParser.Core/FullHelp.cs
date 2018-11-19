// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="FullHelp.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace ArgParser.Core
{
    /// <summary>
    /// Represents a more advanced generic help
    /// </summary>
    /// <seealso cref="ArgParser.Core.SimpleHelp" />
    public class FullHelp : SimpleHelp
    {
        /// <summary>
        /// Adds an example
        /// </summary>
        /// <param name="example">The example.</param>
        public void AddExample(Example example)
        {
            example.ThrowIfArgumentNull(nameof(example));
            if (Examples.Contains(example))
                return;
            Examples.Add(example);
        }

        /// <summary>
        /// Gets or sets the examples.
        /// </summary>
        /// <value>The examples.</value>
        public IList<Example> Examples { get; protected internal set; } = new List<Example>();
        /// <summary>
        /// Gets or sets the long description.
        /// </summary>
        /// <value>The long description.</value>
        public string LongDescription { get; set; }
    }
}