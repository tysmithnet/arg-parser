using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ArgParser.Core.Help;

namespace ArgParser.Core
{
    [DebuggerDisplay("{Id}")]
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
                   new ConsumptionResult(info, 0, null);
        }

        public virtual ConsumptionResult Consume(object instance, ConsumptionRequest request)
        {
            var parameter = Parameters.First(x => x.CanConsume(instance, request.Info).Info != request.Info);
            return parameter.Consume(instance, request);
        }

        public void Reset()
        {
            foreach (var parameter in Parameters) parameter.Reset();
        }

        public Func<object> FactoryFunction { get; set; }
        public ParserHelp Help { get; set; }
        public string Id { get; protected internal set; }
        public IEnumerable<Parameter> Parameters => ParametersInternal.ToList();
        protected internal IList<Parameter> ParametersInternal { get; set; } = new List<Parameter>();

    }

    [DebuggerDisplay("{Id}")]
    public class Parser<T> : Parser
    {
        public Parser(string id) : base(id)
        {
        }

        public new Func<T> FactoryFunction
        {
            get => base.FactoryFunction as Func<T>;
            set => base.FactoryFunction = value as Func<object>;
        }
    }
}