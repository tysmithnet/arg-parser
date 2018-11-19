// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="IterationInfoFactory.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    /// Default iteration info factory
    /// </summary>
    /// <seealso cref="ArgParser.Styles.IIterationInfoFactory" />
    public class IterationInfoFactory : IIterationInfoFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IterationInfoFactory"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public IterationInfoFactory(IContext context)
        {
            Context = context.ThrowIfArgumentNull(nameof(context));
        }

        /// <summary>
        /// Creates an IterationInfo from the supplied request
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>IterationInfo.</returns>
        public IterationInfo Create(IterationInfoRequest request)
        {
            var consumed = request.ChainIdentificationResult.ConsumedArgs;
            return new IterationInfo(request.MutatedArgs,
                consumed.Length); // todo: only consume those that still remain in mutated
        }

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        public IContext Context { get; set; }
    }
}