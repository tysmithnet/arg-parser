using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArgParser.Flavors.Git
{
    public class GitParameterRepository : IGitParameterRepository
    {
        internal List<GitParameter> GitParameters { get; set; }  = new List<GitParameter>();


        public void AddParameter(GitParameter parameter)
        {
            GitParameters.Add(parameter);
        }

        public IEnumerable<Positional> GetPositionals()
        {
            return GitParameters.OfType<Positional>();
        }

        public IEnumerable<Switch> GetSwitches()
        {
            return GitParameters.OfType<Switch>();
        }

        public IEnumerable<BooleanSwitch> GetBooleanSwitches()
        {
            return GetSwitches().OfType<BooleanSwitch>();
        }
    }
}
