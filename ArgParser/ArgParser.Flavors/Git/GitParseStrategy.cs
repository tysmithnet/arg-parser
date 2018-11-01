using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ArgParser.Core;

namespace ArgParser.Flavors.Git
{
    public class GitParseStrategy : DefaultParseStrategy
    {
        public GitParseStrategy(IEnumerable<Func<object>> factoryFuncs = null)
        {
            FactoryFunctions = factoryFuncs?.ToList() ?? new List<Func<object>>();
        }

        public override IParseResult Parse(IEnumerable<IParser> parsers, string[] args)
        {
            var results = ParseInstances(parsers, args);
            return CreateParseResult(results);
        }

        protected override IParseResult CreateParseResult(List<object> results)
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

        public IGitValidatorRepository GitValidatorRepository { get; set; }

        public override IIterationInfoFactory IterationInfoFactory { get; set; }
    }
}