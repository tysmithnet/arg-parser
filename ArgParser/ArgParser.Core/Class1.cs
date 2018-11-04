using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public interface IContext
    {
        IParserRepository ParserRepository { get; }
    }

    public interface IParserRepository
    {
        Parser Create(string id);
        void AddParser(string id, Parser parser);
        Parser GetParser(string id);
        IEnumerable<Parser> GetAll();
    }

    public interface IParseResult
    {
        void When<T>(Action<T> handler);
        void WhenError(Action<IEnumerable<ParseException>> handler);
    }

    public interface IParseStrategy
    {
        IParseResult Parse(string[] args, IContext context);
    }

    public interface IInstanceInspector
    {
        void Inspect(object instance, IContext context);
    }

    public interface IHierarchyRepository
    {
        bool IsParent(string parentParserId, string childParserId);
        void EstablishParentChildRelationship(string parentParserId, string childParserId);
        IEnumerable<Parser> GetAncestors(string parserId);
        Parser Get(string parserId);
    }

    public interface IConsumer
    {
        bool CanConsume(object instance, IterationInfo info);
        IterationInfo Consume(object instance, IterationInfo info);
    }

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

    public abstract class Parameter : IConsumer
    {
        public abstract bool CanConsume(object instance, IterationInfo info);

        public abstract IterationInfo Consume(object instance, IterationInfo info);
    }

    public class IterationInfo
    {
        public string[] Args { get; protected internal set; }
        public int Index { get; set; }

        public IterationInfo(string[] args, int index)
        {
            Args = args ?? throw new ArgumentNullException(nameof(args));
            Index = index;
        }

        public IterationInfo Consume(int num)
        {
            return new IterationInfo(Args, Index + num);
        }
    }

    public class ParseException : Exception
    {
        public ParseException(string message) : base(message)
        {
            
        }
    }
}
