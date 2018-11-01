using System.Collections.Generic;

namespace ArgParser.Flavors.Git
{
    public interface IGitParameterRepository
    {
        void AddParameter(string flavorName, GitParameter parameter);
        IEnumerable<BooleanSwitch> GetBooleanSwitches(string flavorName);
        IEnumerable<GitParameter> GetParameters(string flavorName);
        IEnumerable<Positional> GetPositionals(string flavorName);
        IEnumerable<Switch> GetSwitches(string flavorName);
    }
}