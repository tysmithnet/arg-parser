// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-17-2018
// ***********************************************************************
// <copyright file="DefaultPositionalStrategy.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    /// <summary>
    /// The default positional strategy
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ArgParser.Core.IPositionalStrategy{T}" />
    public class DefaultPositionalStrategy<T> : IPositionalStrategy<T>
    {
        /// <summary>
        /// A history of positionals that have been satisfied already
        /// </summary>
        protected internal ISet<object> Seen = new HashSet<object>();
        /// <summary>
        /// Gets or sets the switch strategy.
        /// </summary>
        /// <value>The switch strategy.</value>
        protected internal ISwitchStrategy<T> SwitchStrategy { get; set; }
        /// <summary>
        /// Gets or sets the switches.
        /// </summary>
        /// <value>The switches.</value>
        protected internal IList<Switch<T>> Switches { get; set; }

        /// <summary>
        /// Consumes the current positional arguments
        /// </summary>
        /// <param name="positionals">The positionals.</param>
        /// <param name="instance">The instance.</param>
        /// <param name="info">The iteration information</param>
        /// <returns>IterationInfo.</returns>
        /// <inheritdoc />
        public IIterationInfo Consume(IList<Positional<T>> positionals, T instance, IIterationInfo info)
        {
            var first = positionals.First(p => !Seen.Contains(p));

            Seen.Add(first);
            var consumed = info.CurOn.TakeWhile((e, i) =>
            {

                var clone = info.Clone();
                clone.Index += i;
                var takeWhile = first.TakeWhile(info, e, i);
                var isSwitch = SwitchStrategy.IsSwitch(Switches, clone) || SwitchStrategy.IsGroup(Switches, clone);
                return takeWhile && !isSwitch;
            }).ToArray();
            first.Transformer?.Invoke(info, instance, consumed);
            info.Index += consumed.Length;
            return info;
        }

        /// <summary>
        /// Determines whether the current location is positional.
        /// </summary>
        /// <param name="positionals">The positionals.</param>
        /// <param name="info">The iteration information</param>
        /// <returns><c>true</c> if the specified positionals is positional; otherwise, <c>false</c>.</returns>
        /// <inheritdoc />
        public bool IsPositional(IList<Positional<T>> positionals, IIterationInfo info)
        {
            return positionals.Any(p => !Seen.Contains(p));
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        /// <inheritdoc />
        public void Reset()
        {
            Seen.Clear();
        }

        /// <summary>
        /// Gets or sets the order of command line element additions.
        /// </summary>
        /// <value>The order of additions.</value>
        protected internal IList<PipelineElement<T>> OrderOfAdditions { get; set; }
    }
}