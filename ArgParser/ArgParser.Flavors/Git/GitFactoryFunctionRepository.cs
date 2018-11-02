using System;
using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Flavors.Git
{
    public class GitFactoryFunctionRepository : IGitFactoryFunctionRepository
    {
        public void AddFactoryFunction(string parserName, Func<object> facFunc)
        {
            parserName.ThrowIfArgumentNull(nameof(parserName));
            facFunc.ThrowIfArgumentNull(nameof(facFunc));

            if (!FactoryFunctions.ContainsKey(parserName))
                FactoryFunctions.Add(parserName, new List<Func<object>>());
            if (!FactoryFunctions[parserName].Contains(facFunc))
                FactoryFunctions[parserName].Add(facFunc);
        }

        public bool Contains(string name) => FactoryFunctions.ContainsKey(name);

        public IEnumerable<Func<object>> GetFactoryFunctions(string parserName)
        {
            parserName.ThrowIfArgumentNull(nameof(parserName));
            if (!FactoryFunctions.ContainsKey(parserName))
                throw new KeyNotFoundException(
                    $"Could not find parser with name={parserName}, are you sure it was added?");
            return FactoryFunctions[parserName].ToList();
        }

        protected internal Dictionary<string, List<Func<object>>> FactoryFunctions { get; set; } =
            new Dictionary<string, List<Func<object>>>();
    }
}