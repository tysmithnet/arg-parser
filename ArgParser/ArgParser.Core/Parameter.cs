// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-20-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-21-2018
// ***********************************************************************
// <copyright file="Parameter.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ArgParser.Core
{
    /// <summary>
    ///     Class Parameter.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ArgParser.Core.IParameter{T}" />
    public class Parameter<T> : Parameter, IParameter<T>
    {
        /// <summary>
        ///     Gets the can handle.
        /// </summary>
        /// <value>The can handle.</value>
        /// <inheritdoc />
        public new CanConsumeCallback<T> CanConsume { get; set; }

        /// <summary>
        ///     Gets the handle.
        /// </summary>
        /// <value>The handle.</value>
        /// <inheritdoc />
        public new ConsumeCallback<T> Consume { get; set; }
    }

    public class Parameter : IParameter
    {
        /// <inheritdoc />
        public CanConsumeCallback CanConsume { get; set; }

        /// <inheritdoc />
        public ConsumeCallback Consume { get; set; }
    }
}