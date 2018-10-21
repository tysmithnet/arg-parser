using System;
using System.Collections.Generic;

namespace ArgParser.Core
{
    public class DefaultParseStrategy : IParseStrategy
    {
        public IList<Func<object>> FactoryFunctions { get; set; } = new List<Func<object>>();
        public IIterationInfoFactory IterationInfoFactory { get; set; } = new DefaultIterationInfoFactory();

        /// <inheritdoc />
        public DefaultParseStrategy(IEnumerable<Func<object>> factoryFuncs = null)
        {
            if (factoryFuncs == null) return;
            foreach (var factoryFunc in factoryFuncs)
            {
                FactoryFunctions.Add(factoryFunc);
            }
        }

        /// <inheritdoc />
        public IParseResult Parse(IEnumerable<IParser> parsers, string[] args)
        {
            var results = new List<object>();
            foreach (var parser in parsers)
            {
                foreach (var factoryFunction in FactoryFunctions)
                {
                    var info = IterationInfoFactory.Create(args);
                    var instance = factoryFunction();
                    bool hasFailed = false;
                    int last = 0;
                    while (!hasFailed && !info.IsComplete && parser.CanConsume(instance, info))
                    {
                        info = parser.Consume(instance, info);
                        if (info.Index <= last)
                        {
                            hasFailed = true;
                        }
                    }

                    if (!hasFailed && info.IsComplete)
                    {
                        results.Add(instance);
                    }
                }
            }
            return new DefaultParseResult(results);
        }
    }
}