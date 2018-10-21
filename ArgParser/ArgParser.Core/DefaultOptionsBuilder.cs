using System;

namespace ArgParser.Core
{
    public class DefaultOptionsBuilder<T> : IOptionsBuilder<T>
    {
        protected internal IIterationInfoFactory IterationInfoFactory { get; set; } = new DefaultIterationInfoFactory();
        protected internal Func<T> FactoryFucntion { get; set; }
        protected internal IParser<T> Parser { get; set; } = new DefaultParser<T>();

        /// <inheritdoc />
        public DefaultOptionsBuilder(Func<T> factoryFucntion)
        {
            FactoryFucntion = factoryFucntion ?? throw new ArgumentNullException(nameof(factoryFucntion));
        }

        /// <inheritdoc />
        public T Build(string[] args)
        {
            var info = IterationInfoFactory.Create(args);
            var instance = FactoryFucntion();
            if (Parser.CanHandle(instance, info))
            {
                Parser.Handle(instance, new DefaultIterationInfo());
                return instance;
            }
            return default(T);
        }
    }
}