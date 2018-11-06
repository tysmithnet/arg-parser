﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public class Parser : IConsumer
    {

        public Parser(string id)
        {
            Id = id;
        }

        public void AddParameter(Parameter parameter)
        {
            ParametersInternal.Add(parameter);
        }

        public virtual ConsumptionResult CanConsume(object instance, IterationInfo info)
        {
            return Parameters.Select(x => x.CanConsume(instance, info)).FirstOrDefault(x => x.Info != info) ??
                   new ConsumptionResult(info, 0);
        }

        public virtual ConsumptionResult Consume(object instance, ConsumptionRequest request)
        {
            var parameter = Parameters.First(x => x.CanConsume(instance, request.Info).Info != request.Info);
            return parameter.Consume(instance, request);
        }

        public Func<object> FactoryFunction { get; set; }
        public string Id { get; protected internal set; }
        public IEnumerable<Parameter> Parameters => ParametersInternal.ToList();
        protected internal IList<Parameter> ParametersInternal { get; set; } = new List<Parameter>();
    }

    public class Parser<T> : Parser
    {
        public Parser(string id) : base(id)
        {
        }

        public virtual ConsumptionResult CanConsume(T instance, IterationInfo info)
        {
            return base.CanConsume(instance, info);
        }

        public virtual ConsumptionResult Consume(T instance, ConsumptionRequest request)
        {
            return base.Consume(instance, request);
        }

        public new Func<T> FactoryFunction
        {
            get => base.FactoryFunction as Func<T>;
            set => base.FactoryFunction = () => value();
        }
    }
}