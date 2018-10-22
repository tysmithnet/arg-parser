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
    /// <summary>
    ///     Interface ISwitchContainer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IParameterContainer<out T>
    {
        /// <summary>
        ///     Adds the switch.
        /// </summary>
        /// <param name="parameter">The svitch.</param>
        void AddParameter(IParameter<T> parameter, IGenericHelp help = null);
    }
}