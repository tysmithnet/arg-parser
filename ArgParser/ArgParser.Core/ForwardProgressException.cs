// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 11-04-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="ForwardProgressException.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace ArgParser.Core
{
    /// <summary>
    /// An exception that arises when the iteration does make forward progress
    /// on every consumption. This is to prevent indefinite parsing.
    /// </summary>
    /// <seealso cref="ArgParser.Core.ParseException" />
    public class ForwardProgressException : ParseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForwardProgressException"/> class.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="message">The message.</param>
        public ForwardProgressException(IterationInfo location, string message) : base(message)
        {
            Location = location.ThrowIfArgumentNull(nameof(location));
        }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>The location.</value>
        public IterationInfo Location { get; protected internal set; }
    }
}