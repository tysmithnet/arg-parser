﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ArgParser.Core;
using ArgParser.Core.Validation;

namespace ArgParser.Flavors.Git
{
    public class GitParseStrategy : IParseStrategy
    {
        /// <inheritdoc />
        public GitParseStrategy(IEnumerable<Func<object>> factoryFuncs = null)
        {
            FactoryFunctions = factoryFuncs?.ToList() ?? new List<Func<object>>();
        }

        /// <inheritdoc />
        public virtual IParseResult Parse(IEnumerable<IParser> parsers, string[] args)
        {
            var results = ParseInstances(parsers, args);
            return CreateParseResult(results);
        }

        protected virtual IParseResult CreateParseResult(List<object> results)
        {
            var agg = results.Aggregate(new HashSet<object>(), (set, o) =>
            {
                if (!set.Any())
                {
                    set.Add(o);
                    return set;
                }
                var toBeRemoved = set.Where(x => x.GetType().GetTypeInfo().IsAssignableFrom(o.GetType().GetTypeInfo())).ToList();
                set.ExceptWith(toBeRemoved);
                if(toBeRemoved.Any())
                    set.Add(o);
                return set;
            });
            return new DefaultParseResult(agg.ToList());
        }

        protected virtual List<object> ParseInstances(IEnumerable<IParser> parsers, string[] args)
        {
            var results = new List<object>();
            var list = parsers.ToList();
            foreach (var factoryFunction in FactoryFunctions)
            foreach (var parser in list)
            {
                parser.Reset();
                var info = IterationInfoFactory.Create(args);
                var instance = factoryFunction();
                if (results.Any(r => r.GetType() == instance.GetType()))
                    continue;
                var hasFailed = false;
                var last = 0;
                while (!hasFailed && !info.IsComplete && parser.CanConsume(instance, info))
                {
                    info = parser.Consume(instance, info);
                    if (info.Index <= last)
                        hasFailed = true;
                }

                var validationResults =
                    Validators.Where(v => v.CanValidate(instance)).Select(v => v.Validate(instance));
                var passedValidation = validationResults.All(r => r.IsSuccess);
                if (!hasFailed && info.IsComplete && passedValidation) results.Add(instance);
            }

            return results;
        }

        public IList<Func<object>> FactoryFunctions { get; set; }
        public virtual IIterationInfoFactory IterationInfoFactory { get; set; } = new GitIterationInfoFactory();
        public virtual IList<IValidator> Validators { get; set; } = new List<IValidator>();
    }
}