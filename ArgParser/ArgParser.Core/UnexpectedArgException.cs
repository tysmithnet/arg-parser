// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 11-04-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="UnexpectedArgException.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace ArgParser.Core
{
    /// <summary>
    /// An exception that occurs when an argument that is not expected is encountered
    /// </summary>
    /// <seealso cref="ArgParser.Core.ParseException" />
    public class UnexpectedArgException : ParseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnexpectedArgException"/> class.
        /// </summary>
        /// <param name="location">The location.</param>
        public UnexpectedArgException(IterationInfo location) : base($"Encountered unexpected argument={location.Current}")
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