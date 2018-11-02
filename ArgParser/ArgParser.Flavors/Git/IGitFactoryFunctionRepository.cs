using System;
using System.Collections.Generic;

namespace ArgParser.Flavors.Git
{
    public interface IGitFactoryFunctionRepository
    {
        void AddFactoryFunction(string parserName, Func<object> facFunc);
        IEnumerable<Func<object>> GetFactoryFunctions(string parserName);
        bool Contains(string name);
    }
}