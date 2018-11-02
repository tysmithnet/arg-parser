using System;
using System.Collections.Generic;

namespace ArgParser.Flavors.Git
{
    public interface IGitFactoryFunctionRepository
    {
        void AddFactoryFunction(string parserName, Func<object> facFunc);
        bool Contains(string name);
        IEnumerable<Func<object>> GetFactoryFunctions(string parserName);
    }
}