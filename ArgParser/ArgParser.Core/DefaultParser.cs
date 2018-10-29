// ***********************************************************************
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
    public class DefaultParser<T> : DefaultParser, IParser<T>, IParameterContainer<T>
    {
        public virtual void AddParameter(IParameter<T> parameter, IGenericHelp help = null)
        {
            base.AddParameter(parameter, help);
        }

        public virtual bool CanConsume<TSub>(TSub instance, IIterationInfo info) where TSub : T
        {
            return base.CanConsume(instance, info);
        }

        public virtual IIterationInfo Consume<TSub>(TSub instance, IIterationInfo info) where TSub : T
        {
            return base.Consume(instance, info);
        }
    }

    public class DefaultParser : IParser, IParameterContainer
    {
        public virtual void AddHelp(IGenericHelp help)
        {
            Help = help;
            HelpBuilder.AddGenericHelp(help);
        }

        public virtual void AddParameter(IParameter parameter, IGenericHelp help = null)
        {
            Parameters.Add(parameter);
            if (help == null)
                return;
            HelpBuilder.AddParameter(help.Name,
                help.Examples?.SelectMany(e => e?.Usage).Where(x => !x.IsNullOrWhiteSpace()).ToArray(),
                help.ShortDescription);
        }

        public virtual bool CanConsume(object instance, IIterationInfo info)
        {
            return Parameters.Any(p => p.CanConsume(instance, info)) ||
                   (BaseParser?.CanConsume(instance, info) ?? false);
        }


        public virtual IIterationInfo Consume(object instance, IIterationInfo info)
        {
            var first = Parameters.FirstOrDefault(p => p.CanConsume(instance, info));
            var result = first?.Consume(instance, info) ?? BaseParser?.Consume(instance, info);
            if (result == null)
                throw new InvalidOperationException(
                    $"CanConsume determined that instance: {instance}, could be consumed, but failed during consumption");
            return result;
        }
        
        public IParser BaseParser { get; set; }

        /// <inheritdoc />
        public IGenericHelp Help { get; protected internal set; }

        public DefaultHelpBuilder HelpBuilder { get; set; } = new DefaultHelpBuilder();

        public IList<IParameter> Parameters { get; set; } = new List<IParameter>();
    }
}