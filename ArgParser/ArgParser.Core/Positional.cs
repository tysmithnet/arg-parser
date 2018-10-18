// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-16-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-17-2018
// ***********************************************************************
// <copyright file="Positional.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ArgParser.Core
{
    /// <summary>
    ///     Represents a token whose purpose is derived from its position in arguments that do not belong to another switch
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="PipelineElement{T}" />
    public class Positional<T> : PipelineElement<T>
    {
    }
}