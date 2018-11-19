// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="ParserChainIdentificationStrategy.cs" company="ArgParser.Styles">
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
    ///     Chain identifcation strategy that will allow for aliases to be used
    /// </summary>
    /// <seealso cref="ArgParser.Styles.IParserChainIdentificationStrategy" />
    public class ParserChainIdentificationStrategy : IParserChainIdentificationStrategy
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ParserChainIdentificationStrategy" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ParserChainIdentificationStrategy(IContext context)
        {
            Context = context.ThrowIfArgumentNull(nameof(context));
        }

        /// <summary>
        ///     Identifies the parser chain from the request
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>ChainIdentificationResult.</returns>
        public ChainIdentificationResult Identify(ChainIdentificationRequest request)
        {
            var args = request.ThrowIfArgumentNull(nameof(request)).Args.PreventNull().ToArray();
            if (!args.Any())
                return new ChainIdentificationResult(Context.RootParser().ToEnumerableOfOne(), new string[0]);

            List<string> Helper(List<string> history)
            {
                var index = history.Count;
                if (index >= args.Length)
                    return history;

                var cur = args[index];
                var potentials = cur.ToEnumerableOfOne().Concat(request.Context.AliasRepository.Lookup(cur));
                var results = new List<List<string>>();
                foreach (var potential in potentials)
                {
                    var left = history.Any() ? history.Last() : Context.HierarchyRepository.GetRoot();
                    var right = potential;
                    if (!Context.HierarchyRepository.IsParent(left, right)) continue;
                    var copy = history.ToList();
                    copy.Add(right);
                    results.Add(Helper(copy));
                }

                if (!results.Any())
                    return history.ToList();

                var bestMatch = results.GroupBy(x => x.Count).OrderByDescending(x => x.Key).First();
                if (bestMatch.Count() > 1)
                    throw new AmbiguousCommandChainException(bestMatch.Select(x => x.ToList()).ToList());
                return bestMatch.Single().ToList();
            }

            var found = Helper(new List<string>());
            if (!found.Any())
                return new ChainIdentificationResult(Context.RootParser().ToEnumerableOfOne(), new string[0]);
            var chain = Context.PathToRoot(found.Last()).Reverse().ToList();
            return new ChainIdentificationResult(chain, args.Take(chain.Count - 1).ToArray());
        }

        /// <summary>
        ///     Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        public IContext Context { get; set; }
    }
}