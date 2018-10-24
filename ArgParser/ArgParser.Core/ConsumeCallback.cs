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
    public delegate IIterationInfo ConsumeCallback<in T>(T instance, IIterationInfo info);
    public delegate IIterationInfo ConsumeCallback(object instance, IIterationInfo info);
}