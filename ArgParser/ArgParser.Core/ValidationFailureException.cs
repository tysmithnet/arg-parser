// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-16-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-16-2018
// ***********************************************************************
// <copyright file="ValidationFailureException.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ArgParser.Core
{
    /// <summary>
    ///     Class ValidationFailureException.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class ValidationFailureException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValidationFailureException" /> class.
        /// </summary>
        /// <param name="errors">The errors.</param>
        /// <exception cref="ArgumentNullException">errors</exception>
        public ValidationFailureException(List<string> errors)
        {
            ValidationMessages =
                new ReadOnlyCollection<string>(errors ?? throw new ArgumentNullException(nameof(errors)));
        }

        /// <summary>
        ///     Gets the validation messages.
        /// </summary>
        /// <value>The validation messages.</value>
        public IReadOnlyCollection<string> ValidationMessages { get; internal set; }
    }
}