using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ArgParser.Core;
using ArgParser.Core.Validation;

namespace ArgParser.Flavors.Git
{
    public class GitParseStrategy<T> : GitParseStrategy, IParseStrategy<T>
    {
        public GitParseStrategy(IGitContext context) : base(context)
        {
        }

        public IParseResult Parse(IEnumerable<IParser<T>> parsers, string[] args) => base.Parse(parsers, args);
    }

    public class GitParseStrategy : IParseStrategy
    {
        protected internal Dictionary<object, List<ParseException>> ParseErrors { get; set; } = new Dictionary<object, List<ParseException>>();

        public GitParseStrategy(IGitContext context)
        {
            Context = context.ThrowIfArgumentNull(nameof(context));
        }

        public IGitContext Context { get; set; }

        public virtual IParseResult Parse(IEnumerable<IParser> parsers, string[] args)
        {
            var results = ParseInstances(parsers, args);
            return CreateParseResult(results);
        }

        public virtual List<object> ParseInstances(IEnumerable<IParser> parsers, string[] args)
        {
            args.ThrowIfArgumentNull(nameof(args));
            var parsersList = parsers.ThrowIfArgumentNull(nameof(parsers)).OfType<GitParser>().ToList();

            var funcList = parsersList.Where(x => Context.FactoryFunctionRepository.Contains(x.Name))
                .SelectMany(x => Context.FactoryFunctionRepository.GetFactoryFunctions(x.Name)).ToList();

            var validators = parsersList.Where(x => Context.ValidatorRepository.Contains(x.Name)).SelectMany(x => Context.ValidatorRepository.GetValidators(x.Name));

            var results = new List<object>();
            foreach (var factoryFunction in funcList)
            foreach (var parser in parsersList)
            {
                foreach (var p in parsersList) // todo: maybe parsersList belies some abstraction?
                {
                    p.Reset();
                }
                var info = IterationInfoFactory.Create(args).ToGitIterationInfo();
                var instance = factoryFunction();
                if (results.Any(r => r.GetType() == instance.GetType()))
                    continue;
                var hasFailed = false;
                var last = 0;
                while (!hasFailed && !info.IsComplete && parser.CanConsume(instance, info))
                {
                    info = parser.Consume(instance, info).ToGitIterationInfo();
                    if (info.Index <= last) hasFailed = true;
                }
            }

            return results;
        }


        public virtual IIterationInfoFactory IterationInfoFactory { get; set; } = new GitIterationInfoFactory();

        protected internal IParseResult CreateParseResult(List<object> results)
        {
            var agg = results.Aggregate(new HashSet<object>(), (set, o) =>
            {
                if (!set.Any())
                {
                    set.Add(o);
                    return set;
                }

                var toBeRemoved = set.Where(x => x.GetType().GetTypeInfo().IsAssignableFrom(o.GetType().GetTypeInfo()))
                    .ToList();
                set.ExceptWith(toBeRemoved);
                if (toBeRemoved.Any())
                    set.Add(o);
                return set;
            });
            return new DefaultParseResult(agg.ToList(), ParseErrors.Values.SelectMany(x => x));
        }
    }
}