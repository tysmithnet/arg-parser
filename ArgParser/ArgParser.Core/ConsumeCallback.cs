// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-20-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-20-2018
// ***********************************************************************
// <copyright file="HandlerCallback.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ArgParser.Core
{
    /// <summary>
    ///     Delegate HandlerCallback
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="instance">The instance.</param>
    /// <param name="info">The information.</param>
    /// <returns>IIterationInfo.</returns>
    public delegate IIterationInfo ConsumeCallback<in T>(T instance, IIterationInfo info);

    public delegate IIterationInfo ConsumeCallback(object instance, IIterationInfo info);
}