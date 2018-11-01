using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Flavors.Git
{
    public class GitFactoryFunctionRepository : IGitFactoryFunctionRepository
    {
        protected internal Dictionary<string, List<Func<object>>> FactoryFunctions { get; set; } = new Dictionary<string, List<Func<object>>>();
        /// <inheritdoc />
        public void AddFactoryFunction(string parserName, Func<object> facFunc)
        {
            if(parserName == null)
                throw new ArgumentNullException(nameof(parserName));
            if(facFunc == null)
                throw new ArgumentNullException(nameof(facFunc));

            if(!FactoryFunctions.ContainsKey(parserName))
                FactoryFunctions.Add(parserName, new List<Func<object>>());
            if (!FactoryFunctions[parserName].Contains(facFunc))
                FactoryFunctions[parserName].Add(facFunc);
        }

        /// <inheritdoc />
        public IEnumerable<Func<object>> GetFactoryFunctions(string parserName)
        {
            if (parserName == null)
                throw new ArgumentNullException(nameof(parserName));
            if (!FactoryFunctions.ContainsKey(parserName))
                throw new KeyNotFoundException($"Could not find parser with name={parserName}, are you sure it was added?");
            return FactoryFunctions[parserName].ToList();
        }
    }
}