// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-20-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-21-2018
// ***********************************************************************
// <copyright file="ISwitch.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ArgParser.Core
{
    /// <summary>
    ///     Represents something of significance to the parser
    ///     For example, in <code>git commit -am "message"</code> there are several parameters
    ///     1. commit indicates that the git-commit application should run
    ///     2. -am indicates that all files should be staged
    ///     3. -am "message" indicates that the staged files should be committed using the message "message"
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IParameter<in T> : IParameter
    {
        /// <summary>
        ///     Gets the can handle.
        /// </summary>
        /// <value>The can handle.</value>
        new CanConsumeCallback<T> CanConsume { get; }

        /// <summary>
        ///     Gets the handle.
        /// </summary>
        /// <value>The handle.</value>
        new ConsumeCallback<T> Consume { get; }
    }

    public interface IParameter
    {
        CanConsumeCallback CanConsume { get; }
        ConsumeCallback Consume { get; }
    }
}