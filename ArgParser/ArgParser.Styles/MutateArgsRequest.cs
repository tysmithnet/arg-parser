// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="MutateArgsRequest.cs" company="ArgParser.Styles">
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
    ///     Represents a request to mutate the arguments before consuming
    /// </summary>
    public class MutateArgsRequest
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MutateArgsRequest" /> class.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="chain">The chain.</param>
        /// <param name="context">The context.</param>
        public MutateArgsRequest(string[] args, IEnumerable<Parser> chain, IContext context)
        {
            Args = args.ThrowIfArgumentNull(nameof(args));
            Chain = chain.ThrowIfArgumentNull(nameof(chain)).ToList();
            Context = context.ThrowIfArgumentNull(nameof(context));
        }

        /// <summary>
        ///     Gets or sets the arguments.
        /// </summary>
        /// <value>The arguments.</value>
        public string[] Args { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the chain of parsers that are going to consume the arguments
        /// </summary>
        /// <value>The chain.</value>
        public IList<Parser> Chain { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        public IContext Context { get; protected internal set; }
    }
}