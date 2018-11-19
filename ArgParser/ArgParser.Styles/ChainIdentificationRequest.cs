// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="ChainIdentificationRequest.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    /// Request to identify the chain of parsers capable of consuming some arguments
    /// </summary>
    public class ChainIdentificationRequest
    {
        /// <summary>
        /// Gets or sets the arguments.
        /// </summary>
        /// <value>The arguments.</value>
        public string[] Args { get; protected internal set; }
        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        public IContext Context { get; protected internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChainIdentificationRequest"/> class.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="context">The context.</param>
        public ChainIdentificationRequest(string[] args, IContext context)
        {
            Args = args.ThrowIfArgumentNull(nameof(args));
            Context = context.ThrowIfArgumentNull(nameof(context));
        }
    }
}