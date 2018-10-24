﻿// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-20-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-21-2018
// ***********************************************************************
// <copyright file="DefaultParser.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using ArgParser.Core.Help;

namespace ArgParser.Core
{
    public class DefaultParser : IParser, IParameterContainer
    {
        public void AddHelp(IGenericHelp help)
        {
            Help = help;
            HelpBuilder.AddGenericHelp(help);
        }

        public void AddParameter(IParameter parameter, IGenericHelp help = null)
        {
            Parameters.Add(parameter);
            if (help == null) return;
            HelpBuilder.AddParameter(help.Name, help.Examples.SelectMany(x => x.Usage).ToArray(),
                help.ShortDescription);
        }

        public bool CanConsume(object instance, IIterationInfo info)
        {
            return Parameters.Any(p => p?.CanConsume.Invoke(instance, info) ?? false) ||
                   (BaseParser?.CanConsume(instance, info) ?? false);
        }

        public IIterationInfo Consume(object instance, IIterationInfo info)
        {
            var first = Parameters.FirstOrDefault(p => p.CanConsume?.Invoke(instance, info) ?? false);
            var result = first?.Consume?.Invoke(instance, info) ?? BaseParser?.Consume(instance, info);
            if (result == null)
                throw new InvalidOperationException(
                    $"CanConsume determined that instance: {instance}, could be consumed, but failed during consumption");
            return result;
        }

        public IParser BaseParser { get; set; }

        public IGenericHelp Help { get; protected internal set; }

        public DefaultHelpBuilder HelpBuilder { get; set; } = new DefaultHelpBuilder();

        public IList<IParameter> Parameters { get; set; } = new List<IParameter>();

    }

    public class DefaultParser<T> : DefaultParser, IParser<T>, IParameterContainer<T>
    {
        /// <inheritdoc />
        public bool CanConsume<TSub>(TSub instance, IIterationInfo info) where TSub : T
        {
            return base.CanConsume(instance, info);
        }

        /// <inheritdoc />
        public IIterationInfo Consume<TSub>(TSub instance, IIterationInfo info) where TSub : T
        {
            return base.Consume(instance, info);
        }

        /// <inheritdoc />
        public void AddParameter(IParameter<T> parameter, IGenericHelp help = null)
        {
            base.AddParameter(parameter, help);
        }
    }
}