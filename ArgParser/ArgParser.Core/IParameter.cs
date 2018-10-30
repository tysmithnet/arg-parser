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
    public interface IParameter<in T> : IParameter
    {
        bool CanConsume(T instance, IIterationInfo info);
        IIterationInfo Consume(T instance, IIterationInfo info);
    }

    public interface IParameter
    {
        bool CanConsume(object instance, IIterationInfo info);
        IIterationInfo Consume(object instance, IIterationInfo info);
        void Reset();
    }
}