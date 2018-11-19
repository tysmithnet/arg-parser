// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="ArgsMutator.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    /// Something capable of mutating arguments based on the request. It will split apart groups of boolean parameters
    /// and massage the format of arguments.
    /// </summary>
    /// <seealso cref="ArgParser.Styles.IArgsMutator" />
    public class ArgsMutator : IArgsMutator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgsMutator"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ArgsMutator(IContext context)
        {
            Context = context.ThrowIfArgumentNull(nameof(context));
        }

        /// <summary>
        /// Mutates the arguments based on the request
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Mutated arguments</returns>
        public string[] Mutate(MutateArgsRequest request)
        {
            var allSwitchesForChain = request.Chain.SelectMany(x => x.Parameters).OfType<Switch>().ToList();
            var booleanSwitches = allSwitchesForChain.OfType<BooleanSwitch>().ToList();
            var others = allSwitchesForChain.Except(booleanSwitches).ToList();
            var booleanLetters = booleanSwitches.Where(s => s.Letter.HasValue).Select(x => x.Letter.Value.ToString())
                .Join("");
            var otherLetters = others.Where(s => s.Letter.HasValue).Select(x => x.Letter.Value.ToString()).Join("");
            if (!booleanLetters.Any())
                return request.Args;
            List<string> groups;
            if (others.Any())
                groups = request.Args.Where(a => Regex.IsMatch(a, $"-[{booleanLetters}]+[{otherLetters}]?")).ToList();
            else
                groups = request.Args.Where(a => Regex.IsMatch(a, $"-[{booleanLetters}]+")).ToList();
            var copy = request.Args.ToList();
            foreach (var g in groups)
                for (var i = 0; i < copy.Count; i++)
                {
                    var c = copy[i];
                    if (g.Contains(c))
                    {
                        copy.RemoveAt(i);
                        var letters = c.Substring(1).ToCharArray().Reverse();
                        foreach (var letter in letters) copy.Insert(i, $"-{letter}");
                    }
                }

            return copy.ToArray();
        }

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        public IContext Context { get; set; }
    }
}