﻿using System;
using System.Collections.Generic;
using System.Linq;
using ArgParser.Core.Validation;

namespace ArgParser.Core
{
    public class DefaultParseStrategy<T> : DefaultParseStrategy, IParseStrategy<T>
    {
        // ReSharper disable once SuspiciousTypeConversion.Global
        /// <inheritdoc />
        public DefaultParseStrategy(IEnumerable<Func<T>> factoryFuncs = null, IEnumerable<IValidator> validators = null) : base(factoryFuncs.Cast<Func<object>>(), validators)
        {
        }

        public IParseResult Parse(IEnumerable<IParser<T>> parsers, string[] args) => base.Parse(parsers, args);
    }

    public class DefaultParseStrategy : IParseStrategy
    {
        protected internal Dictionary<object, List<ParseException>> ParseErrors { get; set; } = new Dictionary<object, List<ParseException>>();

        public DefaultParseStrategy(IEnumerable<Func<object>> factoryFuncs = null, IEnumerable<IValidator> validators = null)
        {
            foreach (var factoryFunc in factoryFuncs.PreventNull())
                AddFactoryFunction(factoryFunc);
            foreach(var valiator in validators.PreventNull())
                AddValidator(valiator);
        }

        private void AddValidator(IValidator valiator)
        {
            Validators.Add(valiator);
        }

        private void AddFactoryFunction(Func<object> factoryFunc)
        {
            FactoryFunctions.Add(factoryFunc);
        }

        public virtual IParseResult Parse(IEnumerable<IParser> parsers, string[] args)
        {
            var results = ParseInstances(parsers, args);
            return CreateParseResult(results);
        }

        public virtual List<object> ParseInstances(IEnumerable<IParser> parsers, string[] args)
        {
            var results = new List<object>();
            var list = parsers.ToList();
            foreach (var factoryFunction in FactoryFunctions)
            foreach (var parser in list)
            {
                foreach (var p in list)
                {
                    p.Reset();
                }
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

                var validationResults = ValidateResults(instance).ToList();
                var passedValidation = HasPassedValidation(validationResults);
                if (!passedValidation)
                {
                    ParseErrors.Add(instance, new List<ParseException>(validationResults.Where(x => x.Errors != null).SelectMany(x => x.Errors)));
                }
                if (!hasFailed && info.IsComplete && passedValidation)
                    results.Add(instance);
            }

            return results;
        }

        protected internal virtual IParseResult CreateParseResult(List<object> results) =>
            new DefaultParseResult(results, ParseErrors.Values.SelectMany(x => x));

        protected virtual bool HasPassedValidation(IEnumerable<IValidationResult> validationResults)
        {
            return validationResults.All(r => r.IsSuccess);
        }

        protected virtual IEnumerable<IValidationResult> ValidateResults(object instance)
        {
            var validationResults =
                Validators.Where(v => v.CanValidate(instance)).Select(v => v.Validate(instance));
            return validationResults;
        }

        public IList<Func<object>> FactoryFunctions { get; set; } = new List<Func<object>>();
        public virtual IIterationInfoFactory IterationInfoFactory { get; set; } = new DefaultIterationInfoFactory();
        public virtual IList<IValidator> Validators { get; set; } = new List<IValidator>();
    }
}