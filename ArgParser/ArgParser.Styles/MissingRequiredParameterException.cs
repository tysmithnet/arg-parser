// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-12-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="MissingRequiredParameterException.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    /// Exception that occurs when a parameter was marked as required but it was not consumed in the creation
    /// of the instance
    /// </summary>
    /// <seealso cref="ArgParser.Core.ParseException" />
    public class MissingRequiredParameterException : ParseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingRequiredParameterException"/> class.
        /// </summary>
        /// <param name="requiredParameter">The required parameter.</param>
        /// <param name="instance">The instance.</param>
        public MissingRequiredParameterException(Parameter requiredParameter, object instance) : base(
            CreateExceptionMessage(requiredParameter))
        {
            RequiredParameter = requiredParameter.ThrowIfArgumentNull(nameof(requiredParameter));
            Instance = instance.ThrowIfArgumentNull(nameof(instance));
        }

        /// <summary>
        /// Creates the exception message.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>System.String.</returns>
        private static string CreateExceptionMessage(Parameter parameter)
        {
            if (parameter is Switch casted)
                return
                    $"Expected to find required switch=[-{casted.Letter?.ToString() ?? "<no letter>"}, --{casted.Word ?? "<no word>"}], but did not.";
            return $"Expected to find required positional but did not.";
        }

        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public object Instance { get; protected internal set; }
        /// <summary>
        /// Gets or sets the required parameter.
        /// </summary>
        /// <value>The required parameter.</value>
        public Parameter RequiredParameter { get; protected internal set; }
    }
}