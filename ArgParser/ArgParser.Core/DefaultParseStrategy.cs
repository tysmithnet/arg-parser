using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public class DefaultParseStrategy : IParseStrategy
    {
        /// <inheritdoc />
        public DefaultParseStrategy(IEnumerable<Func<object>> factoryFuncs = null)
        {
            foreach (var factoryFunc in factoryFuncs ?? new Func<object>[0]) FactoryFunctions.Add(factoryFunc);
        }

        /// <inheritdoc />
        public IParseResult Parse(IEnumerable<IParser> parsers, string[] args)
        {
            var results = new List<object>();
            foreach (var parser in parsers)
            foreach (var factoryFunction in FactoryFunctions)
            {
                var info = IterationInfoFactory.Create(args);
                var instance = factoryFunction();
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

            return new DefaultParseResult(results);
        }

        public IList<Func<object>> FactoryFunctions { get; set; } = new List<Func<object>>();
        public IIterationInfoFactory IterationInfoFactory { get; set; } = new DefaultIterationInfoFactory();
        public IList<IValidator> Validators { get; set; } = new List<IValidator>();
    }

    public class DefaultParseStrategy<T> : DefaultParseStrategy, IParseStrategy<T>
    {
        /// <inheritdoc />
        public DefaultParseStrategy(IEnumerable<Func<object>> factoryFuncs = null) : base(factoryFuncs)
        {
        }

        /// <inheritdoc />
        public IParseResult Parse(IEnumerable<IParser<T>> parsers, string[] args)
        {
            return base.Parse(parsers, args);
        }
    }
}