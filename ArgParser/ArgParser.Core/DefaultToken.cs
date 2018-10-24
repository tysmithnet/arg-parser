// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-20-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-21-2018
// ***********************************************************************
// <copyright file="Token.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace ArgParser.Core
{
    /// <summary>
    ///     Class Token.
    /// </summary>
    /// <seealso cref="ArgParser.Core.IToken" />
    /// <seealso cref="System.IEquatable{ArgParser.Core.Token}" />
    public class DefaultToken : IToken, IEquatable<DefaultToken>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DefaultToken" /> class.
        /// </summary>
        /// <param name="raw">The raw.</param>
        /// <exception cref="ArgumentNullException">raw</exception>
        /// <inheritdoc />
        public DefaultToken(string raw)
        {
            Raw = raw ?? throw new ArgumentNullException(nameof(raw));
        }

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        /// <inheritdoc />
        public bool Equals(DefaultToken other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Raw, other.Raw);
        }

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((DefaultToken) obj);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        /// <inheritdoc />
        public override int GetHashCode() => Raw != null ? Raw.GetHashCode() : 0;

        /// <summary>
        ///     Gets the raw.
        /// </summary>
        /// <value>The raw.</value>
        /// <inheritdoc />
        public string Raw { get; }
    }
}