using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public class ParseResult
    {

        protected internal object Instance { get; set; }
        protected internal IterationInfo Info { get; set; }

        /// <inheritdoc />
        public ParseResult(object instance, IterationInfo info)
        {
            Instance = instance ?? throw new ArgumentNullException(nameof(instance));
            Info = info ?? throw new ArgumentNullException(nameof(info));
        }

        public ParseResult When<T>(Action<T> handler)
        {
            if (Instance is T casted)
                handler(casted);
            return this;
        }

        public ParseResult WhenErrored(Action<IterationInfo> errorHandler)
        {
            if (Info.Errors.Any())
            {
                errorHandler?.Invoke(Info);
            }
            return this;
        }
    }
}