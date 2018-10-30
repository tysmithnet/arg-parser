using System;
using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Flavors.Git
{
    public class GitParseResult : IParseResult
    {
        /// <inheritdoc />
        public GitParseResult(IList<object> parsedInstances)
        {
            ParsedInstances = parsedInstances ?? throw new ArgumentNullException(nameof(parsedInstances));
            
        }

        /// <inheritdoc />
        public void When<T>(Action<T> handler)
        {
            foreach (var parsedInstance in ParsedInstances.Where(x => x.GetType() == typeof(T)).OfType<T>())
                handler(parsedInstance);
        }

        public IList<object> ParsedInstances { get; set; }
    }
}