// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 11-04-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="Parser.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ArgParser.Core
{
    /// <summary>
    ///     Represents a named aggregate consumer that is capable of producing
    ///     an instance of some type and consuming arguments to populate it
    /// </summary>
    /// <seealso cref="ArgParser.Core.IConsumer" />
    [DebuggerDisplay("{Id}")]
    public class Parser : IConsumer
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Parser" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public Parser(string id)
        {
            Id = id;
        }

        /// <summary>
        ///     Adds a parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void AddParameter(Parameter parameter)
        {
            ParametersInternal.Add(parameter);
        }

        /// <summary>
        ///     Determines whether this instance can consume the args and populate the provided instance
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="info">The information.</param>
        /// <returns>ConsumptionResult.</returns>
        public virtual ConsumptionResult CanConsume(object instance, IterationInfo info)
        {
            return Parameters.Select(x => x.CanConsume(instance, info)).FirstOrDefault(x => x.Info != info) ??
                   new ConsumptionResult(info, 0, null);
        }

        /// <summary>
        ///     Consumes the args and populate the provided instance
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="request">The request.</param>
        /// <returns>ConsumptionResult.</returns>
        public virtual ConsumptionResult Consume(object instance, ConsumptionRequest request)
        {
            var parameter = Parameters.First(x => x.CanConsume(instance, request.Info).Info != request.Info);
            return parameter.Consume(instance, request);
        }

        /// <summary>
        ///     Resets this instance.
        /// </summary>
        public void Reset()
        {
            foreach (var parameter in Parameters) parameter.Reset();
        }

        /// <summary>
        ///     Gets or sets the factory function.
        /// </summary>
        /// <value>The factory function.</value>
        public Func<object> FactoryFunction { get; set; }

        /// <summary>
        ///     Gets or sets the help.
        /// </summary>
        /// <value>The help.</value>
        public ParserHelp Help { get; set; } = new ParserHelp();

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; protected internal set; }

        /// <summary>
        ///     Gets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        public IEnumerable<Parameter> Parameters => ParametersInternal.ToList();

        /// <summary>
        ///     Gets or sets the internal parameter collection
        /// </summary>
        /// <value>The parameters internal.</value>
        protected internal IList<Parameter> ParametersInternal { get; set; } = new List<Parameter>();
    }

    /// <summary>
    ///     Represents a named aggregate consumer that is capable of producing
    ///     an instance of some type and consuming arguments to populate it
    /// </summary>
    /// <typeparam name="T">The type of the instance this parser can create</typeparam>
    /// <seealso cref="ArgParser.Core.Parser" />
    [DebuggerDisplay("{Id}")]
    public class Parser<T> : Parser
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Parser{T}" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public Parser(string id) : base(id)
        {
        }

        /// <summary>
        ///     Gets or sets the factory function.
        /// </summary>
        /// <value>The factory function.</value>
        public new Func<T> FactoryFunction
        {
            get => base.FactoryFunction as Func<T>;
            set => base.FactoryFunction = value as Func<object>;
        }
    }
}