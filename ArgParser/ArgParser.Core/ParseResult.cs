// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-16-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-17-2018
// ***********************************************************************
// <copyright file="ParseResult.cs" company="ArgParser.Core">
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
    /// Represents the result of parsing arguments by a parser implementation
    /// </summary>
    public class ParseResult
    {

        /// <summary>
        /// Gets or sets the instance being constructed
        /// </summary>
        /// <value>The instance.</value>
        protected internal object Instance { get; set; }
        /// <summary>
        /// Gets or sets the iteration information.
        /// </summary>
        /// <value>The iteration information</value>
        protected internal IIterationInfo Info { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParseResult"/> class.
        /// </summary>
        /// <param name="instance">The instance being constructed.</param>
        /// <param name="info">The iteration information.</param>
        /// <exception cref="System.ArgumentNullException">
        /// instance
        /// or
        /// info
        /// </exception>
        /// <inheritdoc />
        public ParseResult(object instance, IIterationInfo info)
        {
            Instance = instance ?? throw new ArgumentNullException(nameof(instance));
            Info = info ?? throw new ArgumentNullException(nameof(info));
        }

        /// <summary>
        /// Execute an action when the options identified by the type parameter have been constructed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler">The handler.</param>
        /// <returns>ParseResult.</returns>
        public ParseResult When<T>(Action<T> handler)
        {
            if (Instance.GetType() == typeof(T))
            {
                handler((T)Instance);

            }
            return this;
        }

        /// <summary>
        /// Execute an action when the process of constructing the options encountered an error
        /// </summary>
        /// <param name="errorHandler">The error handler.</param>
        /// <returns>ParseResult.</returns>
        public ParseResult WhenErrored(Action<IIterationInfo> errorHandler)
        {
            if (Info.HasErrors)
            {
                errorHandler?.Invoke(Info);
            }
            return this;
        }
    }
}