using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public class Parser : IConsumer
    {
        public void AddParameter(Parameter parameter)
        {
            ParametersInternal.Add(parameter);
        }

        public void AddFactoryFunction(Func<object> func)
        {
            FactoryFunctionsInternal.Add(func);
        }

        public virtual bool CanConsume(object instance, IterationInfo info)
        {
            return ParametersInternal.Any(p => p.CanConsume(instance, info));
        }

        public virtual IterationInfo Consume(object instance, IterationInfo info)
        {
            return ParametersInternal.First(p => p.CanConsume(instance, info)).Consume(instance, info);
        }

        public IEnumerable<Func<object>> FactoryFunctions => FactoryFunctionsInternal.ToList();
        public IList<Func<object>> FactoryFunctionsInternal { get; set; } = new List<Func<object>>();
        public string Id { get; protected internal set; }
        public IEnumerable<Parameter> Parameters => ParametersInternal.ToList();
        protected internal IList<Parameter> ParametersInternal { get; set; } = new List<Parameter>();
    }


}