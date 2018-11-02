using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ArgParser.Core;

namespace ArgParser.Flavors.Git
{
    public class GitParseStrategy : IParseStrategy
    {
        public GitParseStrategy(IGitContext context)
        {
            Context = context.ThrowIfArgumentNull(nameof(context));
        }

        public IParseResult Parse(IEnumerable<IParser> parsers, IEnumerable<Func<object>> factoryFunctions,
            string[] args)
        {
            var strategy = new DefaultParseStrategy(factoryFunctions);
            var results = strategy.ParseInstances(parsers, args);
            return CreateParseResult(results);
        }

        public IParseResult Parse(IEnumerable<IParser> parsers, string[] args) => throw new NotImplementedException();

        protected IParseResult CreateParseResult(List<object> results)
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
            return new DefaultParseResult(agg.ToList());
        }

        public IGitContext Context { get; set; }
    }
}