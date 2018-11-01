using System.Collections.Generic;

namespace ArgParser.Flavors.Git
{
    public interface IGitParameterRepository
    {
        void AddParameter(GitParameter parameter);
        IEnumerable<Positional> GetPositionals();
        IEnumerable<Switch> GetSwitches();
        IEnumerable<BooleanSwitch> GetBooleanSwitches();
    }
}