// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-20-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-20-2018
// ***********************************************************************
// <copyright file="Switch.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ArgParser.Core
{
    /// <summary>
    ///     Class Switch.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ArgParser.Core.ISwitch{T}" />
    public class Switch<T> : ISwitch<T>
    {
        /// <summary>
        ///     Gets the can handle.
        /// </summary>
        /// <value>The can handle.</value>
        /// <inheritdoc />
        public CanHandleCallback<T> CanHandle { get; set; }

        /// <summary>
        ///     Gets the handle.
        /// </summary>
        /// <value>The handle.</value>
        /// <inheritdoc />
        public HandlerCallback<T> Handle { get; set; }
    }
}