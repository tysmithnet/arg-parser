using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public class DefaultParseResult : IParseResult
    {
            
        public DefaultParseResult(IList<object> parsedInstances)
        {
            ParsedInstances = parsedInstances ?? throw new ArgumentNullException(nameof(parsedInstances));
        }

            
        public void When<T>(Action<T> handler)
        {
            foreach (var parsedInstance in ParsedInstances.OfType<T>()) handler(parsedInstance);
        }

        public IList<object> ParsedInstances { get; set; }
    }
}