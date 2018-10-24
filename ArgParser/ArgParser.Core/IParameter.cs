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
        new CanConsumeCallback<T> CanConsume { get; }

        new ConsumeCallback<T> Consume { get; }
    }

    public interface IParameter
    {
        CanConsumeCallback CanConsume { get; }
        ConsumeCallback Consume { get; }
    }
}