using System;
using System.Collections.Generic;

namespace ArgParser.Core
{
    public class DefaultParseStrategy<TParent> : IParseStrategy<TParent>
    {
        public Func<TParent> FactoryFunc { get; set; }
        public IIterationInfoFactory IterationInfoFactory { get; set; } = new DefaultIterationInfoFactory();

        /// <inheritdoc />
        public DefaultParseStrategy(Func<TParent> factoryFunc)
        {
            FactoryFunc = factoryFunc ?? throw new ArgumentNullException(nameof(factoryFunc));
        }

        /// <inheritdoc />
        public IParseResult Parse(IEnumerable<IParser<TParent>> parsers, string[] args)
        {
            var results = new List<object>();
            foreach (var parser in parsers)
            {
                var instance = FactoryFunc();
                var info = IterationInfoFactory.Create(args);
                int last = 0;
                bool hasFailed = false;
                while (!hasFailed && parser.CanConsume(instance, info))
                {
                    info = parser.Consume(instance, info);
                    if (info.Index <= last)
                    {
                        hasFailed = true;
                        continue;
                    }
                    last = info.Index;
                }

                if (!hasFailed)
                {
                    results.Add(instance);
                }
            }
            return new DefaultParseResult(results);
        }

        /// <inheritdoc />
        public IParseResult Parse<TSub>(IEnumerable<IParser<TParent>> parsers, string[] args) where TSub : TParent
        {
            var results = new List<object>();
            foreach (var parser in parsers)
            {
                var instance = FactoryFunc();
                var info = IterationInfoFactory.Create(args);
                int last = 0;
                bool hasFailed = false;
                while (!hasFailed && parser.CanConsume(instance, info))
                {
                    info = parser.Consume(instance, info);
                    if (info.Index <= last)
                    {
                        hasFailed = true;
                        continue;
                    }
                    last = info.Index;
                }

                if (!hasFailed)
                {
                    results.Add(instance);
                }
            }
            return new DefaultParseResult(results);
        }
    }
}