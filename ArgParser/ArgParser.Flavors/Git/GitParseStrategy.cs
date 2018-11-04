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
            // ReSharper disable once PossibleMultipleEnumeration
            parsers.ThrowIfArgumentNull(nameof(parsers));
            // ReSharper disable once PossibleMultipleEnumeration
            factoryFunctions.ThrowIfArgumentNull(nameof(factoryFunctions));
            args.ThrowIfArgumentNull(nameof(args));
            // ReSharper disable once PossibleMultipleEnumeration
            var parsersList = parsers.ToList();
            // ReSharper disable once PossibleMultipleEnumeration
            var funcList = factoryFunctions.ToList();

            if(!funcList.Any())
                return new DefaultParseResult(new List<object>(), new []
                {
                    new NoFactoryFunctionError(), 
                });

            args.ThrowIfArgumentNull(nameof(args));
            var strategy = new DefaultParseStrategy
            {
                FactoryFunctions = funcList
            };
            var results = strategy.ParseInstances(parsersList, args);
            return CreateParseResult(results);
        }

        public IParseResult Parse(IEnumerable<IParser> parsers, string[] args) => throw new NotImplementedException();

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
            return new DefaultParseResult(agg.ToList());
        }

        public IGitContext Context { get; set; }
    }
}