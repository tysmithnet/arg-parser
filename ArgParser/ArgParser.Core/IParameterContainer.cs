// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-20-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-20-2018
// ***********************************************************************
// <copyright file="ISwitchContainer.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using ArgParser.Core.Help;

namespace ArgParser.Core
{
    public interface IParameterContainer
    {
        void AddParameter(IParameter parameter, IGenericHelp help = null);
    }

    public interface IParameterContainer<out T> : IParameterContainer
    {
        void AddParameter(IParameter<T> parameter, IGenericHelp help = null);
    }
}