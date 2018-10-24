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
    public delegate bool CanConsumeCallback<in T>(T instance, IIterationInfo info);
    public delegate bool CanConsumeCallback(object instance, IIterationInfo info);
}