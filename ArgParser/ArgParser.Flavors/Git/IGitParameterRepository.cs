using System.Collections.Generic;

namespace ArgParser.Flavors.Git
{
    public interface IGitParameterRepository
    {
        void AddParameter(string parserName, GitParameter parameter);
        bool Contains(string name);
        IEnumerable<BooleanSwitch> GetBooleanSwitches(string parserName);
        IEnumerable<GitParameter> GetParameters(string parserName);
        IEnumerable<Positional> GetPositionals(string parserName);
        IEnumerable<Switch> GetSwitches(string parserName);
    }
}