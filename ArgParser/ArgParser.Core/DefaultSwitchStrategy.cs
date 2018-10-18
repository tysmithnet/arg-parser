// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-17-2018
// ***********************************************************************
// <copyright file="DefaultSwitchStrategy.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    /// <summary>
    /// Default switch strategy implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ArgParser.Core.ISwitchStrategy{T}" />
    public class DefaultSwitchStrategy<T> : ISwitchStrategy<T>
    {
        /// <summary>
        /// Gets or sets the positional strategy.
        /// </summary>
        /// <value>The positional strategy.</value>
        protected internal IPositionalStrategy<T> PositionalStrategy { get; set; }
        /// <summary>
        /// Gets or sets the positionals.
        /// </summary>
        /// <value>The positionals.</value>
        protected internal IList<Positional<T>> Positionals { get; set; }
        /// <summary>
        /// Gets or sets the switches.
        /// </summary>
        /// <value>The switches.</value>
        protected internal IList<Switch<T>> Switches { get; set; }

        /// <summary>
        /// Consumes the group.
        /// </summary>
        /// <param name="switches">The switches.</param>
        /// <param name="instance">The instance.</param>
        /// <param name="info">The information about the current iteration over the arguments.</param>
        /// <returns>IterationInfo.</returns>
        /// <inheritdoc />
        public IIterationInfo ConsumeGroup(IList<Switch<T>> switches, T instance, IIterationInfo info)
        {
            var switchList = info.Cur.Substring(1).ToCharArray();
            var notLast = switchList.Reverse().Skip(1).Reverse().ToArray();
            foreach (var n in notLast)
            {
                var currentSwitch = switches.Where(x => x.GroupLetter.HasValue).First(x => x.GroupLetter.Value == n);
                currentSwitch.Transformer?.Invoke(info, instance, new string[0]);
            }

            var lastLetter = switchList.Last();
            var lastSwitch = switches.Where(x => x.GroupLetter.HasValue).First(x => x.GroupLetter.Value == lastLetter);
            var consumed = info.Rest.TakeWhile((e, i) =>
            {
                var clone = info.Clone();
                clone.Index += i + 1;
                var takeWhile = lastSwitch.TakeWhile(info, e, i);
                var isSwitch = clone.Index < info.AllArgs.Length && IsSwitch(switches, clone);
                var isGroup = clone.Index < info.AllArgs.Length && IsGroup(switches, clone);
                return takeWhile && !isSwitch && !isGroup;
            }).ToArray();
            lastSwitch.Transformer?.Invoke(info, instance, consumed);
            info.Index += consumed.Length + 1;
            return info;
        }

        /// <summary>
        /// Consume tokens to statisfy the provided switches if they require it
        /// </summary>
        /// <param name="switches">The switches to consider.</param>
        /// <param name="instance">The options object currently being constructed.</param>
        /// <param name="info">The information about the current iteration over the arguments.</param>
        /// <returns>IterationInfo.</returns>
        /// <inheritdoc />
        public IIterationInfo ConsumeSwitch(IList<Switch<T>> switches, T instance, IIterationInfo info)
        {
            var first = switches.First(s => s.IsToken(info));
            var consumed = info.Rest.TakeWhile((e, i) =>
            {
                var clone = info.Clone();
                clone.Index += i + 1;
                var takeWhile = first.TakeWhile(info, e, i);
                var isSwitch = clone.Index < info.AllArgs.Length && IsSwitch(switches, clone);
                var isGroup = clone.Index < info.AllArgs.Length && IsGroup(switches, clone);
                return takeWhile && !isSwitch && !isGroup;
            }).ToArray();
            first.Transformer(info, instance, consumed);
            info.Index += consumed.Length + 1;
            return info;
        }

        /// <summary>
        /// Determines if the current token is a group of switches e.g. grep -rnw
        /// </summary>
        /// <param name="switches">The switches to consider.</param>
        /// <param name="info">The information about the current iteration over the arguments.</param>
        /// <returns><c>true</c> if the current token is a switch group; otherwise, <c>false</c>.</returns>
        public bool IsGroup(IList<Switch<T>> switches, IIterationInfo info)
        {
            if (!info.Cur.StartsWith("-"))
                return false;
            var sansHash = info.Cur.Substring(1).ToCharArray();
            var letters = switches
                .Where(x => x.GroupLetter.HasValue)
                .Select(x => x.GroupLetter.Value)
                .Select(x => x.ToString())
                .ToArray();
                
            return sansHash.All(x => letters.Contains($"{x}"));
        }

        /// <summary>
        /// Determines if the current token is a switch
        /// </summary>
        /// <param name="switches">The switches to consider.</param>
        /// <param name="info">The iteration information</param>
        /// <returns><c>true</c> if the current token is a switch; otherwise, <c>false</c>.</returns>
        /// <inheritdoc />
        public bool IsSwitch(IList<Switch<T>> switches, IIterationInfo info)
        {
            return switches.Any(s => s.IsToken?.Invoke(info) ?? false);
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        /// <inheritdoc />
        public void Reset()
        {
        }
    }
}