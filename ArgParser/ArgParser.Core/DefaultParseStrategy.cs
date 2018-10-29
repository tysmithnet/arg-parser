﻿using System;
using System.Collections.Generic;
using System.Linq;
using ArgParser.Core.Validation;

namespace ArgParser.Core
{
    public class DefaultParseStrategy<T> : DefaultParseStrategy, IParseStrategy<T>
    {
        /// <inheritdoc />
        // ReSharper disable once SuspiciousTypeConversion.Global
        public DefaultParseStrategy(IEnumerable<Func<T>> factoryFuncs = null) : base(factoryFuncs?.Cast<Func<object>>())
        {
        }

        /// <inheritdoc />
        public IParseResult Parse(IEnumerable<IParser<T>> parsers, string[] args) => base.Parse(parsers, args);
    }

    public class DefaultParseStrategy : IParseStrategy
    {
        /// <inheritdoc />
        public DefaultParseStrategy(IEnumerable<Func<object>> factoryFuncs = null)
        {
            foreach (var factoryFunc in factoryFuncs ?? new Func<object>[0]) FactoryFunctions.Add(factoryFunc);
        }

        /// <inheritdoc />
        public virtual IParseResult Parse(IEnumerable<IParser> parsers, string[] args)
        {
            var results = ParseInstances(parsers, args);
            return CreateParseResult(results);
        }

        protected virtual IParseResult CreateParseResult(List<object> results) => new DefaultParseResult(results);

        protected virtual List<object> ParseInstances(IEnumerable<IParser> parsers, string[] args)
        {
            var results = new List<object>();
            var list = parsers.ToList();
            foreach (var factoryFunction in FactoryFunctions)
            foreach (var parser in list)
            {
                var info = IterationInfoFactory.Create(args);
                var instance = factoryFunction();
                if (results.Any(r => r.GetType() == instance.GetType()))
                    continue;
                var hasFailed = false;
                var last = 0;
                while (!hasFailed && !info.IsComplete && parser.CanConsume(instance, info))
                {
                    info = parser.Consume(instance, info);
                    if (info.Index <= last) hasFailed = true;
                }

                var validationResults =
                    Validators.Where(v => v.CanValidate(instance)).Select(v => v.Validate(instance));
                var passedValidation = validationResults.All(r => r.IsSuccess);
                if (!hasFailed && info.IsComplete && passedValidation) results.Add(instance);
            }

            return results;
        }

        public IList<Func<object>> FactoryFunctions { get; set; } = new List<Func<object>>();
        public virtual IIterationInfoFactory IterationInfoFactory { get; set; } = new DefaultIterationInfoFactory();
        public virtual IList<IValidator> Validators { get; set; } = new List<IValidator>();
    }
}