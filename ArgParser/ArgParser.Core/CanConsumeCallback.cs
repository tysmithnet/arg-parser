// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-20-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-21-2018
// ***********************************************************************
// <copyright file="CanHandleCallback.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ArgParser.Core
{
    /// <summary>
    ///     Delegate CanHandleCallback
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="instance">The instance.</param>
    /// <param name="info">The information.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    public delegate bool CanConsumeCallback<in T>(T instance, IIterationInfo info);

    public delegate bool CanConsumeCallback(object instance, IIterationInfo info);
}