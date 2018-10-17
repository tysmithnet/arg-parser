using System;
using System.Collections.Generic;

namespace ArgParser.Core
{
    public class ParseResult
    {
        protected internal Dictionary<Type, Action> Handlers = new Dictionary<Type, Action>();

        protected internal object Instance { get; set; }
        protected internal IList<ParsingError> Errors { get; set; }

        /// <inheritdoc />
        public ParseResult(object instance)
        {
            Instance = instance ?? throw new ArgumentNullException(nameof(instance));
        }

        /// <inheritdoc />
        public ParseResult(object instance, IList<ParsingError> errors)
        {
            Instance = instance ?? throw new ArgumentNullException(nameof(instance));
            Errors = errors ?? throw new ArgumentNullException(nameof(errors));
        }

        public ParseResult When<T>(Action<T> handler)
        {
            // todo: check
            Handlers.Add(typeof(T), );
            return this;
        }
    }
}