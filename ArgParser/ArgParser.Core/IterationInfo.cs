// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 11-04-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="IterationInfo.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    /// <summary>
    ///     An immutable value type that represents the state of read head for the parsing infrastructure
    /// </summary>
    /// <seealso cref="System.IEquatable{ArgParser.Core.IterationInfo}" />
    /// <seealso cref="System.IComparable{ArgParser.Core.IterationInfo}" />
    /// <seealso cref="System.IComparable" />
    public sealed class IterationInfo : IEquatable<IterationInfo>, IComparable<IterationInfo>, IComparable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="IterationInfo" /> class.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="index">The index.</param>
        public IterationInfo(string[] args, int index = 0)
        {
            Args = args.ThrowIfArgumentNull(nameof(args));
            Index = index; // todo: bounds check
        }

        /// <summary>
        ///     Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public int CompareTo(IterationInfo other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            if (!Args.SequenceEqual(other.Args))
                throw new InvalidOperationException($"Both IterationInfo instances must have the same args to compare");
            return Index.CompareTo(other.Index);
        }

        /// <summary>
        ///     Compares to.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="ArgumentException">IterationInfo</exception>
        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            if (!(obj is IterationInfo)) throw new ArgumentException($"Object must be of type {nameof(IterationInfo)}");
            return CompareTo((IterationInfo) obj);
        }

        /// <summary>
        ///     Consumes the specified number of tokens
        /// </summary>
        /// <param name="num">The number of tokens consumed.</param>
        /// <returns>The result of the consumption</returns>
        public IterationInfo Consume(int num) => new IterationInfo(Args, Index + num);

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///     true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise,
        ///     false.
        /// </returns>
        public bool Equals(IterationInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Index == other.Index && Args.SequenceEqual(other.Args);
        }

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((IterationInfo) obj);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Index + 1) % 31337;
                hashCode = Args.Aggregate(hashCode, (i, s) => i ^= s.GetHashCode());
                return hashCode;
            }
        }

        /// <summary>
        ///     Implements the == operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(IterationInfo left, IterationInfo right) => Equals(left, right);

        /// <summary>
        ///     Implements the &gt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(IterationInfo left, IterationInfo right) =>
            Comparer<IterationInfo>.Default.Compare(left, right) > 0;

        /// <summary>
        ///     Implements the &gt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(IterationInfo left, IterationInfo right) =>
            Comparer<IterationInfo>.Default.Compare(left, right) >= 0;

        /// <summary>
        ///     Implements the != operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(IterationInfo left, IterationInfo right) => !Equals(left, right);

        /// <summary>
        ///     Implements the &lt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(IterationInfo left, IterationInfo right) =>
            Comparer<IterationInfo>.Default.Compare(left, right) < 0;

        /// <summary>
        ///     Implements the &lt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(IterationInfo left, IterationInfo right) =>
            Comparer<IterationInfo>.Default.Compare(left, right) <= 0;

        /// <summary>
        ///     Gets or sets the arguments being iterated over.
        /// </summary>
        /// <value>The arguments.</value>
        public string[] Args { get; internal set; }

        /// <summary>
        ///     Gets the argument at the read head
        /// </summary>
        /// <value>The current.</value>
        public string Current => Args[Index];

        /// <summary>
        ///     Gets or sets the index of the read head
        /// </summary>
        /// <value>The index.</value>
        public int Index { get; internal set; }
    }
}