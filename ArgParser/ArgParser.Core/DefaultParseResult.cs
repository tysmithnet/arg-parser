using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public class DefaultParseResult : IParseResult
    {
        public IList<object> ParsedInstances { get; set; }

        /// <inheritdoc />
        public DefaultParseResult(IList<object> parsedInstances)
        {
            ParsedInstances = parsedInstances ?? throw new ArgumentNullException(nameof(parsedInstances));
        }

        /// <inheritdoc />
        public void When<T>(Action<T> handler)
        {
            foreach (T parsedInstance in ParsedInstances.OfType<T>())
            {
                handler(parsedInstance);
            }    
        }
    }
}