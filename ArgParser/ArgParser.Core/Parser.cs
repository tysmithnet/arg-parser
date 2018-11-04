using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public class Parser : IConsumer
    {
        public string Id { get; protected internal set; }

        protected internal IList<Parameter> ParametersInternal { get; set; } = new List<Parameter>();

        public IEnumerable<Parameter> Parameters => ParametersInternal.ToList();

        public void AddParameter(Parameter parameter)
        {
            ParametersInternal.Add(parameter);
        }

        public virtual bool CanConsume(object instance, IterationInfo info)
        {
            return ParametersInternal.Any(p => p.CanConsume(instance, info));
        }

        public virtual IterationInfo Consume(object instance, IterationInfo info)
        {
            return ParametersInternal.First(p => p.CanConsume(instance, info)).Consume(instance, info);
        }
    }
}
